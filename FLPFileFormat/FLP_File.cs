﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
/*
    Deserialization based on 
    https://github.com/LMMS/lmms/blob/master/plugins/flp_import/FlpImport.cpp
    and file format/EventID overview supplied by ImageLine.

    Notably, FLP project files aren't simply a serialized object tree hierarchy that can be dumped and restored.
    They're more like linear script files that, during loading, "result" in the 
    final project state by linear evaluation of the contained commands.
    Additionally the script-"language" is context sensitive, 
    so the same "command" has different results (or crashes FL) depending on prior execution.

    Testing has only been done with a limited number of FL12 projects, so behavior for legacy versions cannot be predicted. 
*/
namespace FLPFileFormat
{
    [Serializable]
    public class FLP_File
    {
        public static bool Dbg_AllowUnknownIDs = false;
        public bool UsesUnicodeStrings = true;

        public enum FLP_Format : short
        {
            FLP_Format_None = -1,      // temporary
            FLP_Format_Song = 0,        // full project
            FLP_Format_Score = 0x10,  // score
            FLP_Format_Auto = FLP_Format_Score + 8,  // automation
            FLP_Format_ChanState = 0x20,     // channel
            FLP_Format_PlugState = 0x30,     // plugin
            FLP_Format_PlugState_Gen = 0x31,
            FLP_Format_PlugState_FX = 0x32,
            FLP_Format_MixerState = 0x40,     // mixer track
            FLP_Format_Patcher = 0x50     // special: tells to Patcherize (internal)
        }
        public const byte FLP_Byte = 0, FLP_Word = 64, FLP_Int = 128, FLP_Text = 192;
        public enum EventID : byte
        {
            //Renamed/Reordered into rough categories for clearer xml semantics
            //Project
            ID_Project_LoopActive = 9,
            ID_Project_ShowInfo = 10,
            ID_Project_Shuffle = 11,
            ID_Project_TimeSig_Num = 17,
            ID_Project_TimeSig_Beat = 18,
            ID_Project_PanVolTab = 23,               // log vol & circular pan tables
            ID_Project_Registered = 28,               // reg version
            ID_Project_APDC = 29,
            ID_Project_TruncateClipNotes = 30,
            ID_Project_EEAutoMode = 31,
            ID_Project_129_NewUnknown = 35,

            ID_Project_FineTempo = FLP_Int + 28,
            ID_Project_MainPitch = FLP_Word + 16,
            ID_Project_Current_Pattern = FLP_Word + 3,

            ID_Project_Title = FLP_Text + 2,
            ID_Project_Comment = FLP_Text + 3,
            ID_Project_Genre = FLP_Text + 14,
            ID_Project_Author = FLP_Text + 15,
            ID_Project_URL = FLP_Text + 5,
            ID_Project_CommentRTF = FLP_Text + 6,  // comments in Rich Text format
            ID_Project_Version = FLP_Text + 7,
            ID_Project_RegName = FLP_Text + 8,  // since 1.3.9 the (scrambled) reg name is stored in the FLP
            ID_Project_DataPath = FLP_Text + 10,
            ID_Project_Time = FLP_Text + 45,

            //ChannelFilters (Categories)
            ID_ChannelFilter_Num = FLP_Int + 17,
            ID_ChannelFilter_CurrentNum = FLP_Int + 18,
            ID_ChannelFilter_Name = FLP_Text + 39,

            //Channel
            OBS_Channel_Name = FLP_Text,
            ID_Channel_New = FLP_Word,
            ID_Channel_Enabled = 0,
            ID_Channel_Type = 21,
            ID_Channel_MixerTrack_Target = 22,
            FL12_3_Channel_LockedMidiController = 32,

            ID_Channel_SampleFileName = FLP_Text + 4,
            ID_Channel_Envelope = FLP_Text + 26, // TODO: Channel Envelope - 17 DWORDs, reverse ADSR/LFO fields
            ID_Channel_Levels = FLP_Text + 27, // pan, vol, pitch, filter, filter type
            ID_Channel_Filter = FLP_Text + 28, // cut, res, type (obsolete)
            ID_Channel_Poly = FLP_Text + 29, // max poly, poly slide, monophonic
            ID_Channel_Parameters = FLP_Text + 23, // TODO: Plugin Parameters - 29 DWORDs, block of various channel params (can grow)
            ID_Channel_Tracking = FLP_Text + 36, // vol/kb tracking
            ID_Channel_LevelOffsets = FLP_Text + 37, // levels offset
            //These all apply to channels (roughly in this order):
            ID_Channel_Delay = FLP_Text + 17,
            ID_Channel_Delay_ModXYChange = FLP_Int + 10, // delay modx, delay mody
            ID_Channel_ShiftTime = FLP_Word + 25, //Channel Time Shift: delays midi notes
            ID_Channel_SwingMix = FLP_Word + 33, //Channel Swing Mix: How much of the global swing is applied to this Channel
            ID_Channel_RootNote = FLP_Int + 7,   //4b Channel root node in int
            FLP_FX = FLP_Word + 5,
            FLP_FX3 = FLP_Word + 22,
            FLP_FXCut = FLP_Word + 7,
            FLP_FXRes = FLP_Word + 19,
            FLP_FXPreamp = FLP_Word + 10,
            FLP_FXDecay = FLP_Word + 11,
            FLP_FXAttack = FLP_Word + 12,
            FLP_FXStDel = FLP_Word + 21,
            FLP_FXSine = FLP_Int + 3,
            FLP_FXFlags = FLP_Word + 6,
            ID_Channel_CutCutBy = FLP_Int + 4, // 2b cuts, 2b cut by
            FLP_LayerFlags = FLP_Int + 16,
            FLP_SampleFlags = FLP_Int + 15,
            FLP_LoopType = 20,

            //Plugin
            ID_Plugin_New = FLP_Text + 20, // TODO: New VST or DirectX plugin - 13 DWORDs 
            ID_Plugin_Parameters = FLP_Text + 21, // TODO: Plugin Block of plugin setting bytes, export to fst?
            ID_Plugin_Color = FLP_Int + 0,
            ID_Plugin_Default_Name = FLP_Text + 9,
            ID_Plugin_Name = FLP_Text + 11,
            ID_Plugin_Icon = FLP_Int + 27,

            //Pattern
            ID_Pattern_New = FLP_Word + 1,
            ID_Pattern_Data = FLP_Word + 4,
            ID_Pattern_Color = FLP_Int + 22,
            ID_Pattern_Name = FLP_Text + 1,
            ID_Pattern_Ctrl_Events = FLP_Text + 31, // automated ctrl events per pattern
            ID_Pattern_Note_Events = FLP_Text + 32, // automated note events per pattern

            //Playlist
            OBS_Playlist_Item = FLP_Int + 1,         // Pos (word) +PatNum (word) (obsolete)
            ID_Playlist_Track_Info = FLP_Text + 46,
            ID_Playlist_Track_Name = FLP_Text + 47,
            ID_Playlist_Events = FLP_Text + 41, // playlist
            ID_Playlist_Selection = FLP_Text + 25, // selection in playlist

            ID_TimeMarker_New = FLP_Int + 20,    // + Time & Mode in higher bits
            ID_TimeMarker_Name = FLP_Text + 13, // time marker name

            //Mixer
            ID_MixerTrack_Icon = FLP_Word + 31,
            ID_MixerTrack_Color = FLP_Int + 21,
            ID_MixerTrack_Input_Channel = FLP_Int + 26,      // FX track input channel
            ID_MixerTrack_Output_Channel = FLP_Int + 19,     // FX track output channel
            ID_MixerTrack_Name = FLP_Text + 12, // FX track name
            ID_MixerTrack_Routing = FLP_Text + 43,
            ID_MixerTrack_Parameters = FLP_Text + 44,
            FL12_3_MixerTrack_Unknown = FLP_Word + 34,

            //BYTE
            FLP_NoteOn = 1,                // +pos
            FLP_MIDIChan = 4,
            FLP_MIDINote = 5,
            FLP_MIDIPatch = 6,
            FLP_MIDIBank = 7,
            FLP_Zipped = 15,
            FLP_UseLoopPoints = 19,
            FLP_SSLength = 25,               // +length
            FLP_SSLoop = 26,
            FLP_FXProps = 27,               // FlipY, ReverseStereo, etc

            //WORD
            FLP_DotVol = FLP_Word + 8,
            FLP_DotPan = FLP_Word + 9,
            FLP_DotNote = FLP_Word + 13,
            FLP_DotPitch = FLP_Word + 14,
            FLP_DotMix = FLP_Word + 15,
            FLP_DotFRes = FLP_Word + 23,
            FLP_DotFCut = FLP_Word + 24,
            FLP_Dot = FLP_Word + 27,
            FLP_DotShift = FLP_Word + 28,
            FLP_DotRel = FLP_Word + 32,
            FLP_LoopEndBar = FLP_Word + 26,
            ID_Channel_LayerParentOf = FLP_Word + 30,

            // DWORD sized (128..191)
            FLP_Echo = FLP_Int + 2,
            FLP_WindowHeight = FLP_Int + 5,
            FLP_WindowWidth = FLP_Int + 6,
            FLP_Reserved = FLP_Int + 8,        // may contain an invalid version info
            FLP_Reverb = FLP_Int + 11,
            FLP_StretchTime = FLP_Int + 12,
            FLP_FineTune = FLP_Int + 14,
            FLP_SongLoopPos = FLP_Int + 24,
            FLP_AUSmpRate = FLP_Int + 25,
            FL12_5_Unknown = FLP_Int + 29,

            // Variable size (192..255)
            //FLP_Undef = 192,               // +Length (VarLengthInt)
            FLP_MIDICtrls = FLP_Text + 16,
            FLP_CtrlRecChan = FLP_Text + 24, // automated controller events
            FLP_NoteRecChan = FLP_Text + 30, // automated note events
            FLP_Ctrl_Init_RecChan = FLP_Text + 33, // init values for automated events
            FLP_Ctrl_Remote_MIDI = FLP_Text + 34, // remote control entry (MIDI)
            FLP_Ctrl_Remote_Internal = FLP_Text + 35, // remote control entry (internal)
            FLP_Ctrl_Remote_Formula = FLP_Text + 38, // remote control entry formula
            FLP_RegBlackList = FLP_Text + 40, // black list of reg codes
            ID_Channel_Articulator = FLP_Text + 42, // channel articulator

            //Obsolete
            OBS_ChanVol = 2,
            OBS_ChanPan = 3,
            OBS_MainVol = 12,
            OBS_FitToSteps = 13,
            OBS_Pitchable = 14,
            OBS_Delay_Flags = 16,
            OBS_nStepsShown = 24,              // steps in stepsequencer
            OBS_RandChan = FLP_Word + 17,
            OBS_MixChan = FLP_Word + 18,
            OBS_OldSongLoopPos = FLP_Word + 20,
            OBS_Tempo = FLP_Word + 2,
            OBS_Tempo_Fine = FLP_Word + 29,
            OBS_MainResCut = FLP_Int + 9,
            OBS_SSNote = FLP_Int + 13,        // SimSynth patch middle note
            OBS_PatAutoMode = FLP_Int + 23,      // obsolete
            OBS_TS404Params = FLP_Text + 18,
            OBS_DelayLine = FLP_Text + 19, // obsolete
            OBS_Reserved2 = FLP_Text + 22 // used once for testing
        }
        
        /*
           FL Studio doesnt need any Channels to load a project.
           Or any Playlisttracks (it adds 34 default ones on saving)
           Or Patterns,Filters
           Or Project description Texts
           Or mixer tracks
           Or any events at all.
           Looks like it will just use default values for that stuff.
           TODO: Possible to delete default value events? (Ordering!)


           TODO: transform serial list to tree
              parent events: project, channel, plugin, mixer track, ???


           TODO: basic scripts
           channel/note histogram, map to scales?
           normalize project:
               - split all patterns into channels
               - sort all clips based on channel into distinct playlist lane
               - map 
        */
        private List<FLP_Event> _events = new List<FLP_Event>();

        //Members
        [XmlElement("Bytes", Type = typeof(FLPE_Bytes))]
        [XmlElement("Channel_Delay", Type = typeof(FLPE_Channel_Delay))]
        [XmlElement("Channel_LevelOffsets", Type = typeof(FLPE_Channel_LevelOffsets))]
        [XmlElement("Channel_Levels", Type = typeof(FLPE_Channel_Levels))]
        [XmlElement("Channel_Poly", Type = typeof(FLPE_Channel_Poly))]
        [XmlElement("Channel_Tracking", Type = typeof(FLPE_Channel_Tracking))]
        [XmlElement("Color", Type = typeof(FLPE_Color))]
        [XmlElement("Ctrl_Init_RecChan", Type = typeof(FLPE_Ctrl_Init_RecChan))]
        [XmlElement("CutCutBy", Type = typeof(FLPE_CutCutBy))]
        [XmlElement("Data", Type = typeof(FLPE_Data))]
        [XmlElement("DelayFRes", Type = typeof(FLPE_DelayFRes))]
        [XmlElement("MixerTrack_Parameters", Type = typeof(FLPE_MixerTrack_Parameters))]
        [XmlElement("MixerTrack_Routing", Type = typeof(FLPE_MixerTrack_Routing))]
        [XmlElement("Pattern_Ctrl_Events", Type = typeof(FLPE_Pattern_Ctrl_Events))]
        [XmlElement("Pattern_Note_Events", Type = typeof(FLPE_Pattern_Note_Events))]
        [XmlElement("Playlist_Events", Type = typeof(FLPE_Playlist_Events))]
        [XmlElement("ID_Playlist_Track_Info", Type = typeof(ID_Playlist_Track_Info))]
        [XmlElement("Project_Time", Type = typeof(FLPE_Project_Time))]
        [XmlElement("Unicode", Type = typeof(FLPE_Unicode))]
        [XmlElement("Val", Type = typeof(FLPE_Val))]
        [XmlElement("Values", Type = typeof(FLPE_Values))]
        public FLP_Event[] Events
        {
            get
            {
                return _events.ToArray();
            }

            set
            {
                _events = new List<FLP_Event>(value);
            }
        }
        public int EventCount {  get { return this._events.Count; } }

        [XmlAttribute]
        public char[] FLPHeaderChunkID { get; set; }
        [XmlAttribute]
        public char[] DataChunkID { get; set; }
        [XmlAttribute]
        public uint HeaderChunkLength { get; set; }
        [XmlAttribute]
        public uint DataChunkLength { get; set; }
        [XmlAttribute]
        public FLP_Format FLPFormat { get; set; }
        [XmlAttribute]
        public ushort BeatDivision { get; set; }
        [XmlAttribute]
        public ushort RackChannelCount { get; set; }
        public byte MAX_MIXERTRACKS = 1+1+125;

        public FLP_File()
        {
            //Empty constructor for XMLSerialization
        }
        public FLP_File(string filename, TextWriter logger = null, Action<string> reporter = null)
        {
            BinaryReader r = new BinaryReader(File.OpenRead(filename));
            this.Deserialize(r, logger, reporter);
            r.Close();
        }
        public long PerformNullTest(string filename)
        {
            MemoryStream temp_stream = new MemoryStream();
            BinaryWriter temp_writer = new BinaryWriter(temp_stream);
            this.Serialize(temp_writer);
            temp_writer.Close();

            MemoryStream read_stream = new MemoryStream(temp_stream.ToArray());
            BinaryReader r = new BinaryReader(File.OpenRead(filename));
            BinaryReader r2 = new BinaryReader(read_stream);
            while (r.BaseStream.Position < r.BaseStream.Length)
            {
                byte orig=r.ReadByte();
                byte serialized = r2.ReadByte();
                if (orig != serialized)
                {
                    long error_pos = r.BaseStream.Position;
                    r.Close(); r2.Close();
                    return error_pos;
                }
            }
            r.Close(); r2.Close();
            return -1;
        }

        public void Deserialize(BinaryReader r, TextWriter logger, Action<string> reporter=null)
        {
            //01. read header
            this.FLPHeaderChunkID = r.ReadChars(4); //"FLhd" FL header
            this.HeaderChunkLength = r.ReadUInt32(); //Should be 6
            this.FLPFormat = (FLP_Format)r.ReadInt16();
            this.RackChannelCount = r.ReadUInt16(); //Unused
            this.BeatDivision = r.ReadUInt16(); //ppq

            this.DataChunkID = r.ReadChars(4); //"FLdt" FL data
            this.DataChunkLength = r.ReadUInt32();

            //02. Parse sequence of events next...
            FLP_Event next_event = null;
            do
            {
                long p = r.BaseStream.Position;
                if (p == r.BaseStream.Length) break;
                try
                {
                    next_event = FLP_Event.FromEventID((FLP_File.EventID)r.ReadByte(),false);
                    if(logger!=null) logger.WriteLine((p) + " B = " + next_event.Id);
                    if (next_event != null)
                    {
                        next_event.ParentProject = this;
                        next_event.Deserialize(r);
                        this._events.Add(next_event);
                    }
                }
                catch (InvalidDataException ivd) //If at first we don't succeed...
                {
                    //Reset the stream
                    if (reporter != null)
                        reporter("Error occured: " + ivd.Message + ". Falling back to generic parsing...");
                    r.BaseStream.Seek(p, SeekOrigin.Begin);
                    next_event = FLP_Event.FromEventID((FLP_File.EventID)r.ReadByte(), true); // try try again (this time with fallback mode)
                    if (logger != null) logger.WriteLine((p) + " B = " + next_event.Id);
                    if (next_event != null)
                    {
                        next_event.ParentProject = this;
                        next_event.Deserialize(r);
                        this._events.Add(next_event);
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidDataException("Error while loading [" + r.BaseStream.Position + "B]: " + e.Message);
                }
            }
            while (next_event != null);

            if (r.BaseStream.Position < r.BaseStream.Length)
            {
                throw new InvalidDataException("Unable to complete loading: " + r.BaseStream.Position + "<" + r.BaseStream.Length+" bytes parsed.");
            }
        }

        public void Serialize(BinaryWriter w)
        {
            w.Write(this.FLPHeaderChunkID);
            w.Write(this.HeaderChunkLength);
            w.Write((short)this.FLPFormat);
            w.Write(this.RackChannelCount);
            w.Write(this.BeatDivision);


            MemoryStream temp_stream = new MemoryStream();
            BinaryWriter temp_writer = new BinaryWriter(temp_stream);
            for (int event_index = 0; event_index < this._events.Count; event_index++)
            {
                this._events[event_index].Serialize(temp_writer);
            }
            temp_writer.Close();
            byte[] data = temp_stream.ToArray();

            w.Write(this.DataChunkID);
            w.Write(data.Length);
            w.Write(data);
        }

        #region Helper functions
        public List<FLP_Event> GetEventsWithIDs(params FLP_File.EventID[] ids)
        {
            return _events.FindAll(delegate (FLP_Event e) { return Array.IndexOf(ids, e.Id) != -1; });
        }
        #endregion

        #region Removal
        public void RemoveRedundantEvents()
        {
            this._events = _events.FindAll(delegate (FLP_Event e) { return !e.IsDefault(); });
        }
        public FLP_Event Seek(FLP_Event origin, FLP_File.EventID id, bool forward = true, ISet<FLP_File.EventID> abort_on = null)
        {
            int i = ((origin == null) ? 0 : this._events.IndexOf(origin));
            if (i == -1) return null;
            int d = forward ? 1 : -1;
            //Always perform the first step...
            if (origin != null) i += d;
            for (; i >= 0 && i < _events.Count; i += d)
            {
                if (_events[i].Id == id) return _events[i];
                if (abort_on != null && abort_on.Contains(_events[i].Id)) return null;
            }
            return null;
        }

        public void RemoveFL125Events(bool verbose = false)
        {
            foreach (FLP_Event n in this.GetEventsWithIDs(EventID.FL12_5_Unknown))
            {
                if (verbose) Console.WriteLine("Removing event: " + n.ToString());
                _events.Remove(n);
            }
        }
        public void RemoveFL123Events(bool verbose = false)
        {
            foreach (FLP_Event n in this.GetEventsWithIDs(EventID.FL12_3_Channel_LockedMidiController, EventID.FL12_3_MixerTrack_Unknown))
            {
                if (verbose) Console.WriteLine("Removing event: " + n.ToString());
                _events.Remove(n);
            }
        }

        // This is now part of Fl 12.5! This is still useful for batching though
        public void RemoveUnusuedPatterns(bool verbose=false)
        {
            //1. Identify used patterns:
            HashSet<int> used_pattern_ids = new HashSet<int>();
            List<FLP_Event> playlists = this.GetEventsWithIDs(EventID.ID_Playlist_Events);

            if (verbose) Console.WriteLine("Discovering pattern usage...");
            foreach (FLPE_Playlist_Events pl in playlists)
            {
                if (verbose) Console.WriteLine("in playlist: " + pl.ToString());
                foreach (PlaylistClip c in pl.PlaylistItems)
                {
                    int pid = c.ClipSource - c.PatternClipOffset;
                    if (pid > 0 && !used_pattern_ids.Contains(pid))
                    { //>=0?
                        if (verbose) Console.WriteLine("Found used pattern with ID " + pid + " in " + c.ToString());
                        used_pattern_ids.Add(pid);
                    }
                }
            }

            //2. Select pattern related events
            foreach (FLP_Event n in this.GetEventsWithIDs(EventID.ID_Pattern_Color, EventID.ID_Pattern_Name, EventID.ID_Pattern_Note_Events, EventID.ID_Pattern_Ctrl_Events))
                if (!used_pattern_ids.Contains(((FLPE_Val)this.Seek(n, EventID.ID_Pattern_New, false)).V))
                {
                    if (verbose) Console.WriteLine("Removing pattern event: "+n.ToString());
                    _events.Remove(n);
                }
            //3. Finally remove unused Pattern headers.
            foreach (FLPE_Val n in this.GetEventsWithIDs(EventID.ID_Pattern_New))
                if (!used_pattern_ids.Contains(n.V))
                {
                    if (verbose) Console.WriteLine("Removing pattern header: " + n.ToString());
                    _events.Remove(n);
                }
        }
        #endregion

        #region Mixer Manipulation
        public FLP_File ExtractMixer()
        {

            FLP_File mixer = new FLP_File();
            mixer.FLPFormat = FLP_File.FLP_Format.FLP_Format_PlugState;
            mixer.HeaderChunkLength = this.HeaderChunkLength;
            mixer.FLPHeaderChunkID = this.FLPHeaderChunkID;
            mixer.BeatDivision = this.BeatDivision;
            mixer.DataChunkID = this.DataChunkID;
            mixer.RackChannelCount = 0;
            /*
            // TODO
            foreach (FLP_Event n in this.GetEventsWithIDs(EventID.ID_Pattern_Color, EventID.ID_Pattern_Name, EventID.ID_Pattern_Note_Events, EventID.ID_Pattern_Ctrl_Events))
                if (!used_pattern_ids.Contains(((FLPE_Val)this.Seek(n, EventID.ID_Pattern_New, false)).V))
                    _events.Remove(n);
                    */
           return mixer;
        }
        public IEnumerable<FLP_Event> GetMixerEvents()
        {
            FLP_Event project_apdc = this.Seek(null, EventID.ID_Project_APDC, true);
            FLP_Event ctrl_init = this.Seek(project_apdc, EventID.FLP_Ctrl_Init_RecChan, true);
            List<FLP_Event> mixer_events = new List<FLP_Event>();
            int start = _events.IndexOf(project_apdc), end = _events.IndexOf(ctrl_init) + 1;
            for (int i = start; i < end; i++)
            {
                mixer_events.Add(_events[i]);
            }
            return mixer_events;
        }

        public void SetMixerEvents(IEnumerable<FLP_Event> new_mixer)
        {
            int start = this.DeleteMixerItems(false);
            _events.InsertRange(start, new_mixer);
        }

        public int DeleteMixerItems(bool verbose = false)
        {
            FLP_Event project_apdc = this.Seek(null, EventID.ID_Project_APDC, true);
            FLP_Event ctrl_init = this.Seek(project_apdc, EventID.FLP_Ctrl_Init_RecChan, true);
            int start = _events.IndexOf(project_apdc), end = _events.IndexOf(ctrl_init) + 1;
            if (verbose) Console.WriteLine("Removing " + (end - start) + " events from " + start + " to " + end);
            _events.RemoveRange(start, end - start);
            return start;
        }
        #endregion

        #region Normalization
        public void NormalizePlaylistBySource(bool verbose = false)
        {
            FLPE_Playlist_Events playlist = (FLPE_Playlist_Events)this.Seek(null, EventID.ID_Playlist_Events, true);
            if (playlist == null) return; // "No Playlist data located, possibly corrupt or invalid file?";


            Dictionary<ushort, List<PlaylistClip>> sources = new Dictionary<ushort, List<PlaylistClip>>();

            foreach (PlaylistClip clip in playlist.PlaylistItems)
            {
                if (!sources.ContainsKey(clip.ClipSource))
                {
                    sources[clip.ClipSource] = new List<PlaylistClip>();
                }
                sources[clip.ClipSource].Add(clip);
            }
            ushort[] sorted = new ushort[sources.Keys.Count];
            sources.Keys.CopyTo(sorted, 0);
            //Select attribute to sort by: id, name, count, size in bytes
            Array.Sort(sorted, delegate (ushort a, ushort b) { return -sources[a].Count.CompareTo(sources[a].Count); });

            foreach (ushort source in sorted)
            {
                ushort new_track = (ushort)(198 - (Array.IndexOf<ushort>(sorted, source) % 198));
                foreach (PlaylistClip clip in sources[source])
                {
                    clip.PlaylistTrack = new_track;
                }
            }
        }

        public void PropagatePatternsToRackChannels()
        {
            //Step 1: For each generator:
            foreach (FLP_Event e_newchannel in this.GetEventsWithIDs(EventID.ID_Channel_New))
            {
                int channel = ((FLPE_Val)e_newchannel).V;

                foreach (FLP_Event pattern_notes in this.GetEventsWithIDs(EventID.ID_Pattern_Note_Events))
                {
                    ushort[] rc = ((FLPE_Pattern_Note_Events)pattern_notes).GetRackChannels();
                    if (rc.Length != 1 || rc[0] != channel) continue;
                    
                    int p_index = ((FLPE_Val)this.Seek(pattern_notes, EventID.ID_Pattern_New, false)).V;
                    FLPE_Unicode p_name = (FLPE_Unicode)this.Seek(e_newchannel, EventID.ID_Plugin_Name, true);
                    foreach (FLPE_Unicode e_pattern_name in this.GetEventsWithIDs(EventID.ID_Pattern_Name))
                    {
                        int namep_index = ((FLPE_Val)this.Seek(e_pattern_name, EventID.ID_Pattern_New, false)).V;
                        if (namep_index == p_index)
                        {
                            p_name.Text = e_pattern_name.Text;
                            break;
                        }
                    }

                    FLPE_Color p_color = (FLPE_Color)this.Seek(e_newchannel, EventID.ID_Plugin_Color, true);
                    foreach (FLPE_Color e_pattern_color in this.GetEventsWithIDs(EventID.ID_Pattern_Color))
                    {
                        int namep_index = ((FLPE_Val)this.Seek(e_pattern_color, EventID.ID_Pattern_New, false)).V;
                        if (namep_index == p_index)
                        {
                            p_color.HexColor = e_pattern_color.HexColor;
                            break;
                        }
                    }

                    break;
                }
            }
        }

        public void PropagateRackChannelToMixerTracks()
        {
            /*
	            ID_Channel_MixerTrack_Target
	            ->ID_Channel_New ->ID_Plugin_Name / ID_Plugin_Color
	            indexing into mixer tracks how?
            */
        }
        #endregion

        #region Statistics
        public string GetEventStatistics()
        {
            Dictionary<EventID, int[]> hist = new Dictionary<EventID, int[]>();

            int totalsize = 0;
            foreach (FLP_Event e in this.Events)
            {
                if (!hist.ContainsKey(e.Id)) hist.Add(e.Id, new int[2]);
                int[] d = hist[e.Id];
                d[0]++;
                MemoryStream temp_stream = new MemoryStream();
                BinaryWriter temp_writer = new BinaryWriter(temp_stream);
                e.Serialize(temp_writer);
                temp_writer.Close();
                byte[] data = temp_stream.ToArray();
                d[1] += data.Length;
                totalsize += data.Length;
            }

            string r = "Total Events: " + this.EventCount + " (" + Math.Round(totalsize * 1.00 / 1024, 3) + " kb), " + hist.Keys.Count + " unique IDs:\n";

            EventID[] sorted = new EventID[hist.Keys.Count];
            hist.Keys.CopyTo(sorted, 0);
            //Select attribute to sort by: id, name, count, size in bytes
            Array.Sort(sorted, delegate (EventID a, EventID b) { return -hist[a][0].CompareTo(hist[b][0]); });

            foreach (EventID id in sorted)
            {
                int[] d = hist[id];
                r += d[0] + "x\t" + Math.Round(d[1] * 100.0 / totalsize, 3) + "%\t" + Math.Round(d[1] * 1.00 / 1024, 3) + " kb\t" + id + "\n";
            }
            return r;
        }

        public string GetPlaylistStatistics()
        {
            FLP_Event pl=this.Seek(null, EventID.ID_Playlist_Events, true);
            if (pl == null || !(pl is FLPE_Playlist_Events)) return "No or unparsable Playlist data located, possibly incompatible or corrupted file.";
            FLPE_Playlist_Events playlist = (FLPE_Playlist_Events)pl;

            List <FLP_Event> track_infos = GetEventsWithIDs(EventID.ID_Playlist_Track_Info);

            Dictionary<uint, List<PlaylistClip>> hist = new Dictionary<uint, List<PlaylistClip>>();
            HashSet<ushort> sources = new HashSet<ushort>();

            foreach (PlaylistClip clip in playlist.PlaylistItems)
            {
                if (!hist.ContainsKey(clip.PlaylistTrack))
                {
                    hist[clip.PlaylistTrack] = new List<PlaylistClip>();
                }
                hist[clip.PlaylistTrack].Add(clip);
                sources.Add(clip.ClipSource);
            }

            string r = "PL [ALL]\t unique: " + sources.Count + "\t total: " + playlist.PlaylistItems.Length + "\n";

            HashSet<EventID> abort = new HashSet<EventID>(new EventID[] { EventID.ID_Playlist_Track_Info });
            for (uint playlisttrack = 199; playlisttrack <200; playlisttrack--) //Abusing unsigned int underflow here
            {
                if (!hist.ContainsKey(playlisttrack)) continue;
                ID_Playlist_Track_Info t_i = null;
                foreach (FLP_Event i in track_infos) if (i is ID_Playlist_Track_Info && ((ID_Playlist_Track_Info)i).TrackNumber == (199 - playlisttrack)) { t_i = (ID_Playlist_Track_Info)i; break; }
                FLPE_Unicode t_n = (FLPE_Unicode)this.Seek(t_i, EventID.ID_Playlist_Track_Name, true, abort);

                HashSet<ushort> track_sources = new HashSet<ushort>();
                List<PlaylistClip> track_clips = hist[playlisttrack];
                uint pos_first = uint.MaxValue, pos_last = uint.MinValue;
                foreach (PlaylistClip clip in track_clips)
                {
                    track_sources.Add(clip.ClipSource);
                    if (clip.Position < pos_first) pos_first = clip.Position;
                    //TODO: account for windowing?
                    if (clip.Position + clip.Duration > pos_last) pos_last = clip.Position + clip.Duration;
                }
                r += "T #" + (t_i == null ? (199 - playlisttrack) : t_i.TrackNumber) + "\t unique: " + track_sources.Count + "\t total: " + track_clips.Count + "\t [" + pos_first + "-" + pos_last + "]\t'" + (t_n == null ? "" : t_n.Text) + "'\t(" + (t_i == null ? "Default" : "" + t_i.HexColor) + ")\n";
            }
            return r;
        }

        public string GetPatternStatistics()
        {
            List<FLP_Event> pattern_inits = GetEventsWithIDs(EventID.ID_Pattern_New);
            Dictionary<int, FLP_Event[]> pattern_data = new Dictionary<int, FLP_Event[]>();

            //Assemble the pattern information...
            HashSet<EventID> abort = new HashSet<EventID>(new EventID[] { EventID.ID_Pattern_New });
            long count_notes = 0, count_ctrlevents = 0;
            foreach (FLP_Event pattern_id in pattern_inits)
            {
                if (!(pattern_id is FLPE_Val)) continue;
                int pid = ((FLPE_Val)pattern_id).V;
                if (!pattern_data.ContainsKey(pid)) pattern_data[pid] = new FLP_Event[4];
                FLPE_Unicode name = (FLPE_Unicode)this.Seek(pattern_id, EventID.ID_Pattern_Name, true, abort);
                if (name != null) pattern_data[pid][0] = name;
                FLPE_Color color = (FLPE_Color)this.Seek(pattern_id, EventID.ID_Pattern_Color, true, abort);
                if (color != null) pattern_data[pid][1] = color;
                FLPE_Pattern_Note_Events notes = (FLPE_Pattern_Note_Events)this.Seek(pattern_id, EventID.ID_Pattern_Note_Events, true, abort);
                if (notes != null)
                {
                    pattern_data[pid][2] = notes;
                    count_notes += notes.NoteCount;
                }
                FLPE_Pattern_Ctrl_Events ctrl_events = (FLPE_Pattern_Ctrl_Events)this.Seek(pattern_id, EventID.ID_Pattern_Ctrl_Events, true, abort);
                if (ctrl_events != null)
                {
                    pattern_data[pid][3] = ctrl_events;
                    count_ctrlevents += ctrl_events.EventCount;
                }
            }

            string r = "Patterns: " + pattern_data.Count + "\t Total notes: " + count_notes + ", ctrl events:" + count_ctrlevents + "\n";
            for (int pid = 0; pid < 1000; pid++)
            {
                if (!pattern_data.ContainsKey(pid)) continue;
                //Metadata
                string p_header= "#" + pid + " " + (pattern_data[pid][0] != null ? ((FLPE_Unicode)pattern_data[pid][0]).Text : "Unnamed") + " ("
                    + (pattern_data[pid][1] != null ? ((FLPE_Color)pattern_data[pid][1]).HexColor : "Default") + ")";
                string p_body = "\n";
                //Note data
                if(pattern_data[pid][2] == null)
                {
                    p_header += ", No Note Data";
                }
                else
                {
                    FLPE_Pattern_Note_Events notes = (FLPE_Pattern_Note_Events)pattern_data[pid][2];
                    //How many channels?
                    ushort[] rackchannels = notes.GetRackChannels();
                    p_header += ", "+notes.NoteCount+" notes for " + rackchannels.Length + " channel(s)";
                    foreach(ushort channel in rackchannels)
                    {
                        //Note count/histogram per channel
                        Dictionary<string, List<PatternNote>> histogram = new Dictionary<string, List<PatternNote>>();
                        int count = 0;
                        byte vel_min = 128, vel_max = 0;
                        long vel_sum = 0;
                        foreach (PatternNote n in notes.Notes)
                        {
                            if (n.RackChannel != channel) continue;
                            string k = n.KeyNoOctave();
                            if (!histogram.ContainsKey(k)) histogram[k] = new List<PatternNote>();
                            histogram[k].Add(n);
                            count++;
                            if (n.Velocity < vel_min) vel_min = n.Velocity;
                            if (n.Velocity > vel_max) vel_max = n.Velocity;
                            vel_sum += n.Velocity;
                        }
                        string h = "";
                        foreach (string k in PatternNote.NoteNames) { if (histogram.ContainsKey(k)) h += " " + k + ":" + histogram[k].Count; }
                        //Guess key or if sampler
                        //?Max polyphony
                        p_body += "\tC" + channel + ": " + count + " notes [" + h + " ] Velocity: [" + vel_min + "," + vel_max + "] avg: " + Math.Round(1.0 * vel_sum / count,2) + "\n";
                    }

                }
                //Event data
                if (pattern_data[pid][3] == null)
                {
                    p_header += ", No Ctrl Event Data";
                }
                else
                {
                    FLPE_Pattern_Ctrl_Events ctrl_events = (FLPE_Pattern_Ctrl_Events)pattern_data[pid][3];
                    //How many channels? Problem since i don't know the channel/target parameter semantics yet.
                    p_header += ", " + ctrl_events.EventCount + " ctrl events";
                    //Channel 
                }
                r += p_header + p_body;
            }
            return r;
        }

        public string GetRoutingStatistics()
        {
            /*
                total:
                    active routes, fx count
                per mixer track:
                    in degree, out degree, fx count, 
            */
            return "Still in development...";
        }
        #endregion
    }
}

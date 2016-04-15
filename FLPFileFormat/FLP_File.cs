using System;
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

        public string GetEventStatistics()
        {
            Dictionary<EventID, int[]> hist = new Dictionary<EventID, int[]>();

            for (int event_index = 0; event_index < this._events.Count; event_index++)
            {
            }
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
                totalsize+= data.Length;
            }

            string r = "";
            foreach(EventID id in hist.Keys)
            {
                int[] d = hist[id];
                r += Math.Round(d[1]*100.0/totalsize,3)+"%\t" + d[0] +"x "+ id +   "\t" + Math.Round(d[1]*1.00 / 1024, 3) + " kb\n";
            }
            return r;
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

        public FLP_File()
        {
            //Empty constructor for XMLSerialization
        }
        public FLP_File(string filename, TextWriter logger)
        {
            BinaryReader r = new BinaryReader(File.OpenRead(filename));
            this.Deserialize(r, logger);
            r.Close();
        }

        public void Deserialize(BinaryReader r, TextWriter logger)
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
                try
                {
                    next_event = FLP_Event.FromEventID((FLP_File.EventID)r.ReadByte());
                    if(logger!=null) logger.WriteLine((p/1024) + "kb = " + next_event.Id);
                    if (next_event != null)
                    {
                        next_event.Deserialize(r);
                        this._events.Add(next_event);
                    }
                }
                catch (Exception e)
                {   
                    next_event = null;
                }
            }
            while (next_event != null);
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

        public void RemoveRedundantEvents()
        {
            this._events = _events.FindAll(delegate (FLP_Event e) { return !e.IsDefault(); });
        }
        public FLP_Event Seek(FLP_Event origin, FLP_File.EventID id, bool forward = true)
        {
            int i = this._events.IndexOf(origin);
            if (i == -1) return null;
            int d = forward ? 1 : -1;
            for (; i >= 0 && i < _events.Count; i += d)
            {
                if (_events[i].Id == id) return _events[i];
            }
            return null;
        }
        public List<FLP_Event> GetEventsWithIDs(params FLP_File.EventID[] ids)
        {
            return _events.FindAll(delegate (FLP_Event e) { return Array.IndexOf(ids,e.Id)!=-1; });
        }

        public void RemoveUnusuedPatterns()
        {
            //1. Identify used patterns:
            HashSet<int> used_pattern_ids = new HashSet<int>();
            List<FLP_Event> playlists = this.GetEventsWithIDs(EventID.ID_Playlist_Events);

            foreach (FLPE_Playlist_Events pl in playlists)
            {
                foreach (PlaylistClip c in pl.PlaylistItems)
                {
                    int pid = c.ClipSource - c.PatternClipOffset;
                    if (pid > 0 && !used_pattern_ids.Contains(pid)) //>=0?
                        used_pattern_ids.Add(pid);
                }
            }

            //2. Select pattern related events
            foreach (FLP_Event n in this.GetEventsWithIDs(EventID.ID_Pattern_Color, EventID.ID_Pattern_Name, EventID.ID_Pattern_Note_Events, EventID.ID_Pattern_Ctrl_Events))
                if (!used_pattern_ids.Contains(((FLPE_Val)this.Seek(n, EventID.ID_Pattern_New, false)).V))
                    _events.Remove(n);
            //3. Finally remove unused Pattern headers.
            foreach (FLPE_Val n in this.GetEventsWithIDs(EventID.ID_Pattern_New))
                if (!used_pattern_ids.Contains(n.V))
                    _events.Remove(n);
        }

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

        public void RemoveFL123Events()
        {
            foreach (FLP_Event n in this.GetEventsWithIDs(EventID.FL12_3_Channel_LockedMidiController, EventID.FL12_3_MixerTrack_Unknown))
                _events.Remove(n);
        }

        public void RemoveMixer()
        {
            throw new NotImplementedException();
        }
    }
}

using System.Collections.Generic;
using System.IO;

namespace FLPXML
{
    public class FL_Project
    {
        //Project Members
        public int mainVolumne, mainPitch = 0, numChannels = 0;
        public string ProjectNotes, ProjectTitle;
        //Version
        private string VersionString;
        private int version, versionSpecificFactor;

        //Patterns
        public Dictionary<int, string> PatternNames = new Dictionary<int, string>();
        public int maxPatterns = 0, CurrentPattern = 0, activeEditPattern = 0;

        //Channels
        public List<FL_Channel> RackChannels = new List<FL_Channel>(); //Instruments
        public List<FL_Effect> Effects = new List<FL_Effect>(); //Effects
        public List<FL_EffectChannel> MixerTracks = new List<FL_EffectChannel>(); //Mixer: Fixed size of 105?
        public int CurrentEffectChannel = -1;
        public List<FL_PlaylistItem> PlaylistItems = new List<FL_PlaylistItem>(); //Arrangement

        public static FL_Project LoadFromFLP(string filename)
        {
            return null;
        }
        
    }

    public class FL_Automation
    {
        public int pos = 0, value = 0, channel = 0, control = 0;
    }
    public class FL_ChannelEnvelope
    {
        public float predelay, attack, hold, decay, sustain, release, amount;
    }
    public class FL_PlaylistItem
    {
        public int position = 0, length = 1, pattern = 0;
    }
    public class FL_Plugin
    {
        public byte PluginType = 0;
        public string Name = null;
        public byte[] pluginSettings = new byte[0];

        public void Deserizalize(BinaryReader r)
        {
        }
    }
    public class FL_Channel : FL_Plugin
    {
        //Basic
        public int volume, panning, baseNote, fxChannel = 0, layerParent = -1;
        public byte[] rgbcolor = new byte[] { 100, 100, 100 };

        //SamplerInfo
        public string SampleFileName = null;
        public int sampleamp = 100;
        public bool sampleReversed = false, sampleReversedStereo = false, sampleUseLoopPoints = false;

        //Filter
        public bool filterEnabled = false;
        public int filterType;
        public float filterCut = 10000f, filterRes = 0.1f;
        //Arp
        public bool arpEnabled = false;
        public int arpDir = 0, arpRange = 0, arpType = 0;
        public float arpTime = 100f, arpGate = 100f;

        //Lists
        public List<FL_Automation> channel_automation = new List<FL_Automation>();
        public List<FL_ChannelEnvelope> channel_envelopes = new List<FL_ChannelEnvelope>();
        public List<int> dots = new List<int>(); //?
        public List<PatterNote> notes = new List<PatterNote>();
    }

    public class PatterNote
    {
        int patternID;
        MidiNote note;
    }

    public class MidiNote
    {
        int len, pos, key, vol, pan;
    }

    public class FL_Effect : FL_Plugin
    {
        int fxChannel = 0, fxPosition = 0;
    }

    public class FLP_PlaylistTrack
    {
        public void Deserizalize(BinaryReader r)
        {

        }
    }
    public class FL_EffectChannel
    {
        string Name;
        int volume;
        bool isMuted = false;

        public void Deserizalize(BinaryReader r)
        {
        }
    }
}

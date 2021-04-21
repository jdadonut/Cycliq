namespace Cycliq.Modules
{
    using System.Collections.Generic;
    using DSharpPlus.VoiceNext;
    public class VoiceMangr : Dictionary<ulong, IVoiceMonitor>
    {
        public IVoiceMonitor GetVoiceMonitor(ulong serverid)
        {
            IVoiceMonitor voiceMonitor;
            if (!this.TryGetValue(serverid, out voiceMonitor))
            {
                this.TryAdd<ulong, IVoiceMonitor>(serverid, new IVoiceMonitor { isPlaying = false, isConnected = false });
                return this.GetVoiceMonitor(serverid);
            }
            return voiceMonitor;

        }
    }
    public class IVoiceMonitor
    {
        public Queue<object> Queue { get; set; }
        public VoiceNextConnection Connection { get; set; }
        public VoiceTransmitSink Sink { get; set; }

        public bool isConnected { get; set; }
        public bool isPlaying { get; set; }
    }
}

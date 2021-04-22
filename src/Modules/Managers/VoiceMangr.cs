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
        public bool isPlaying (ulong serverid)
        {
            IVoiceMonitor voiceMonitor = this.GetVoiceMonitor(serverid);
            return voiceMonitor.isPlaying;
        }
        public void SetPlayingTrue(ulong serverid)
        {
            IVoiceMonitor voiceMonitor = this.GetVoiceMonitor(serverid);
            voiceMonitor.isPlaying = true;
            this.Remove(serverid);
            this.TryAdd(serverid, voiceMonitor);
        }
        public void SetPlayingFalse(ulong serverid)
        {
            IVoiceMonitor voiceMonitor = this.GetVoiceMonitor(serverid);
            voiceMonitor.isPlaying = false;
            this.Remove(serverid);
            this.TryAdd(serverid, voiceMonitor);
        }
        public void SetConnectedTrue(ulong serverid, VoiceNextConnection connection, VoiceTransmitSink sink)
        {
            IVoiceMonitor voiceMonitor = this.GetVoiceMonitor(serverid);
            voiceMonitor.isConnected = true;
            voiceMonitor.Connection = connection;
            voiceMonitor.Sink = sink;
            this.Remove(serverid);
            this.TryAdd(serverid, voiceMonitor);
        }
        public void SetConnectedFalse(ulong serverid)
        {
            IVoiceMonitor voiceMonitor = this.GetVoiceMonitor(serverid);
            voiceMonitor.isConnected = false;
            if (voiceMonitor.Connection != null)
            {
                voiceMonitor.Connection.Disconnect();
                voiceMonitor.Connection.Dispose();
            }
            if (voiceMonitor.Sink != null)
            {
                voiceMonitor.Sink.Dispose();
            }
            this.Remove(serverid);
            this.TryAdd(serverid, voiceMonitor);
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

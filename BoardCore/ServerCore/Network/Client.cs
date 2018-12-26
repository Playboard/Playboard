using BoardCore.ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardCore.ServerCore.Network
{
    public abstract class Client
    {
        public abstract Server Server { get; }
        public abstract void SendMessage(IEvent message);
        public abstract void OnReciveMessage(IEvent message);
        public abstract Player PlayerRequestLogin(IEvent message);
        public abstract bool OnValidateClient();
        public virtual void KickClient() => Server.KickClient(this);
    }
}

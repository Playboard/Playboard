using BoardCore.ServerCore;
using BoardCore.ServerCore.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardServer.Servers
{
    public class WebSocketClient : Client
    {
        public override Server Server => throw new NotImplementedException();

        public override void OnReciveMessage(IEvent message)
        {
            throw new NotImplementedException();
        }

        public override bool OnValidateClient()
        {
            throw new NotImplementedException();
        }

        public override Player PlayerRequestLogin(IEvent message)
        {
            throw new NotImplementedException();
        }

        public override void SendMessage(IEvent message)
        {
            throw new NotImplementedException();
        }
    }
}

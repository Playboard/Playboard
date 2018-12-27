using BoardCore.ServerCore.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardServer.Servers
{
    public class WebSocketServer : Server<WebSocketServer>
    {
        public WebSocketServer() : base("Playboard BoardWebsocket server") { }

        public override bool CheckHealth()
        {
            return true;
        }

        public override void KickClient(Client client)
        {
            throw new NotImplementedException();
            
        }
    }
}

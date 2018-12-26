using System;
using System.Collections.Generic;
using System.Text;

namespace BoardCore.ServerCore.Network
{
    public abstract class Server
    {
        public string ServerName { get; private set; }
        public abstract void KickClient(Client client);
        public abstract bool CheckHealth();

        public Server(string ServerName)
        {
            this.ServerName = ServerName;
        }
    }

    public abstract class Server<T> : Server
        where T : Server<T>
    {
        public static T Instance { get; protected set; }
        internal protected Guid ServerUUID { get; set; } = Guid.NewGuid();
        public Server(string ServerName) : base(ServerName)
        {
            Instance = this as T;
            NetworkManager.Instance.RegisterServer(Instance); 
        }
        
    }
}

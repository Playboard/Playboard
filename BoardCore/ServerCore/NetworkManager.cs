using BoardCore.ServerCore.Lobby;
using BoardCore.ServerCore.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardCore.ServerCore
{
    public class NetworkManager
    {
        public static readonly NetworkManager Instance = new NetworkManager();
        public Dictionary<Type, Server> RunningServers = new Dictionary<Type, Server>();
        private NetworkManager() { }

        public void RegisterServer<T>(T instance) where T : Server
        {
            RunningServers.Add(typeof(T), instance);
        }

        public void RegisterPlayer(Player player)
        {
            LobbyEvents.Instance.RaiseEventAsync(new LobbyPlayerJoinEvent(player));
        }
    }
}

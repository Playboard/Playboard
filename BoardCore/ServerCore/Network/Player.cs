using BoardCore.ServerCore.Lobby;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardCore.ServerCore.Network
{
    [Serializable]
    public class Player
    {
        public string Name;
        public Guid UID;
        [NonSerialized]
        public Client Client;

        public Player(string name, Guid uid, Client client)
        {
            Name = name;
            UID = uid;
            Client = client;

            LobbyEvents.Instance.BindEvent<LobbyPlayerJoinEvent>(x => Client.SendMessage(x));
            LobbyEvents.Instance.BindEvent<LobbyPlayerLeaveEvent>(x => Client.SendMessage(x));
        }
    }
}

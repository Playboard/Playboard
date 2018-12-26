using BoardCore.ServerCore.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BoardCore.ServerCore.Lobby
{
    public class Lobby
    {
        public static readonly Lobby Instance = new Lobby();
        public LinkedList<Player> Players = new LinkedList<Player>();
        public LinkedList<Room> Rooms = new LinkedList<Room>();

        private Lobby()
        {
            
        }

        public void CreateRoom(Player player, out Room room)
        {
            var index = -1;
            lock (Rooms)
            {
                room = new Room();
                Rooms.AddLast(room);
                index = Rooms.Count;
            }
            if (index == 1) room = null;
            else room.InitialRoom(index);
        }

        public void PlayerJoin(Player player)
        {
            lock (Players)
            {
                if (Players.Any(p => p.Name == player.Name))
                {
                    player.Client.SendMessage(new LobbyPlayerAlreadyExistEvent());
                    player.Client.KickClient();
                }
                else
                {
                    Players.AddLast(player);
                }
            }
        }
    }
}

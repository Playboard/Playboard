using BoardCore.ServerCore;
using BoardCore.ServerCore.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardCore.ServerCore.Lobby
{
    public interface ILobbyEvents : IEvent { }

    public class LobbyEvents : BaseEventDispatcher<ILobbyEvents>
    {
        public static readonly LobbyEvents Instance = new LobbyEvents();
        private LobbyEvents()
        {
            EventDispatcher.Instance.RegisterNewDispatcher(GetType());
        }
    }

    public struct LobbyPlayerJoinEvent : ILobbyEvents
    {
        public Player Player { get; }
        public LobbyPlayerJoinEvent(Player player)
        {
            Player = player;
        }
    }

    public struct LobbyPlayerLeaveEvent : ILobbyEvents
    {
        public Player Player { get; }
        public LobbyPlayerLeaveEvent(Player player)
        {
            Player = player;
        }
    }

    public struct LobbyPlayerAlreadyExistEvent : ILobbyEvents
    {
        
    }

    public struct LobbyPlayerCreateRoomEvent
    {
        public int RoomId { get; }
        public LobbyPlayerCreateRoomEvent(int roomId)
        {
            this.RoomId = roomId;
        }
    }
}

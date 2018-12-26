using BoardCore.GameCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardCore.ServerCore.Lobby
{
    public class Room
    {

        public Game CurrentGame { get; private set; }
        public RoomEvents EventBus { get; private set; }
        public int Index { get; private set; }

        /// <summary>
        /// 房间在创建时
        /// <para>1. 传入房间号</para>
        /// <para>2. 创建房间所属的EventBus</para>
        /// </summary>
        public Room() { }

        public void InitialRoom(int Index)
        {
            this.Index = Index;
            EventBus = RoomEvents.CreateRoomEventBus(Index);
        }

        public void InitialGame()
        {

        }

        public void RemoveRoom()
        {
            RoomEvents.AllRoomEventDispatcher.RemoveDispatcher(Index);
        }
    }
}

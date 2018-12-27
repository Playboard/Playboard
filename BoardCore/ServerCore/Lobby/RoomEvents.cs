using System;
using System.Collections.Generic;
using System.Text;

namespace BoardCore.ServerCore.Lobby
{
    public interface IRoomEvent : IEvent { }

    public class RoomEvents : BaseEventDispatcher<int, IRoomEvent>
    {
        public static readonly EventDispatcher<int> AllRoomEventDispatcher = new EventDispatcher<int>();

        public int Index { get; private set; }

        public static RoomEvents CreateRoomEventBus(int Index) => new RoomEvents(AllRoomEventDispatcher, Index);

        public RoomEvents(EventDispatcher<int> dispatcher, int Index) : base(dispatcher)
        {
            Dispatcher.RegisterNewDispatcher(Index);
            this.Index = Index;
        }

        public void RaiseEvent<Event>(IRoomEvent @event) where Event : IRoomEvent => base.RaiseEvent<Event>(Index, @event);
        public void RaiseEventAsync<Event>(IRoomEvent events) where Event : IRoomEvent => base.RaiseEventAsync<Event>(Index, events);
        public void BindEvent<Event>(EventHandlerFunc<Event> handler) where Event : IRoomEvent => base.BindEvent<Event>(Index, handler);
    }

    public struct RoomGameSelectEvent : IRoomEvent
    {

    }

    public struct PlayerJoinRoomEvent : IRoomEvent
    {
        public string Name { get; set; }
        public PlayerJoinRoomEvent(string Name)
        {
            this.Name = Name;
        }
    }

    public struct RoomCreateEvent : IRoomEvent
    {

    }

    public struct RoomDestoryEvent : IRoomEvent
    {

    }
}

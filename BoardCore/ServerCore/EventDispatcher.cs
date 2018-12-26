using BoardCore.ServerCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoardCore.ServerCore
{
    public interface IEvent { }

    public class EventDispatcherTaskScheduler : TaskScheduler
    {
        private readonly LinkedList<Task> tasks = new LinkedList<Task>();
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return tasks;
        }

        protected override void QueueTask(Task task)
        {
            var thread = new Thread(() => { TryExecuteTask(task); });
            thread.Start();
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return TryExecuteTask(task);
        }
    }

    public abstract class BaseEventDispatcher<TKey, T> where T : IEvent
    {
        protected EventDispatcher<TKey> Dispatcher { get; private set; }
        public BaseEventDispatcher(EventDispatcher<TKey> dispatcher)
        {
            this.Dispatcher = dispatcher;
        }

        /// <summary>
        /// public virtual for sub-classes override
        /// </summary>
        /// <typeparam name="Event"></typeparam>
        /// <param name="event"></param>
        public virtual void RaiseEventAsync<Event>(TKey eventType, T @event) where Event : T
        {
            Dispatcher.RaiseEventAsync(eventType, @event);
        }
        
        /// <summary>
        /// public virtual for sub-classes override
        /// </summary>
        /// <param name="event"></param>
        public virtual void RaiseEvent<Event>(TKey eventType, T @event) where Event : T
        {
            this.Dispatcher.RaiseEvent(eventType, @event);
        }

        /// <summary>
        /// regist class for event bind
        /// </summary>
        /// <param name="handler"></param>
        public virtual void BindEvent<Event>(TKey eventType, EventHandlerFunc<Event> handler) where Event : T
        {
            this.Dispatcher.RegisterEventHandler(eventType, handler);
        }
    }

    public abstract class BaseEventDispatcher<T> : BaseEventDispatcher<Type, T> where T : IEvent
    {
        public BaseEventDispatcher() : base(EventDispatcher.Instance)
        {
        }

        /// <summary>
        /// public virtual for sub-classes override
        /// </summary>
        /// <typeparam name="Event"></typeparam>
        /// <param name="event"></param>
        public virtual void RaiseEventAsync<Event>(Event @event) where Event : T
        {
            base.RaiseEventAsync<Event>(GetType(), @event);
        }

        /// <summary>
        /// public virtual for sub-classes override
        /// </summary>
        /// <typeparam name="Event"></typeparam>
        /// <param name="event"></param>
        public virtual void RaiseEvent<Event>(Event @event) where Event : T
        {
            base.RaiseEvent<Event>(GetType(), @event);
        }

        /// <summary>
        /// regist class for event bind
        /// </summary>
        /// <typeparam name="Event"></typeparam>
        /// <param name="handler"></param>
        public void BindEvent<Event>(EventHandlerFunc<Event> handler) where Event : T
        {
            base.BindEvent<Event>(GetType(), handler);
        }
    }

    /// <summary>
    /// the generic handler impl
    /// </summary>
    /// <typeparam name="T">which typeof Event should handle</typeparam>
    /// <param name="event">the event fired you target handler</param>
    /// <returns></returns>
    public delegate void EventHandlerFunc<Event>(Event @event) where Event : IEvent;

    public class HandlerList : LinkedList<object>
    {

    }

    /// <summary>
    /// typedef for LinkedList
    /// </summary>
    public class Dispatcher<T> : Dictionary<T, HandlerList>
    {

    }

    public abstract class EventDispatcher
    {
        public static readonly EventDispatcher<Type> Instance = new EventDispatcher<Type>();
    }

    /// <summary>
    /// [Singleton] Global event dispatcher
    /// </summary>
    public class EventDispatcher<TKey> : EventDispatcher
    {
        private Dictionary<TKey, Dispatcher<Type>> dispatchers = new Dictionary<TKey, Dispatcher<Type>>();

        private readonly TaskScheduler tasks = new EventDispatcherTaskScheduler();

        public EventDispatcher()
        {

        }

        ///// <summary>
        ///// Register new event type[event dispatcher]
        ///// </summary>
        ///// <typeparam name="EventDispatcher">New dispatcher classes</typeparam>
        ///// <returns></returns>
        //public void RegisterNewDispatcher<EventDispatcher, TEvent>() where EventDispatcher : BaseEventDispatcher<TEvent> where TEvent : IEvent
        //{
        //    RegisterNewDispatcher();
        //}

        /// <summary>
        /// Register new event type
        /// </summary>
        /// <param name="TDispatch"></param>
        public void RegisterNewDispatcher(TKey t)
        {
            if (dispatchers.ContainsKey(t)) return;
            else dispatchers.Add(t, new Dispatcher<Type>());
            return;
        }

        /// <summary>
        /// Get all binder of this event
        /// </summary>
        /// <typeparam name="Event">Target event</typeparam>
        /// <param name="eventType">Event dispatcher</param>
        /// <returns></returns>
        public HandlerList GetHandlerList<Event>(TKey eventType)
        {
            return GetHandlerList(eventType, typeof(Event));
        }

        /// <summary>
        /// Get all binder of this event
        /// </summary>
        /// <param name="eventType">Event dispatcher</param>
        /// <param name="event">Target event</param>
        /// <returns></returns>
        public HandlerList GetHandlerList(TKey eventType, Type @event)
        {
            return GetDispatcher(eventType)[@event];
        }

        /// <summary>
        /// Get dispathcer by type
        /// </summary>
        /// <param name="eventType">type</param>
        /// <returns></returns>
        public Dispatcher<Type> GetDispatcher(TKey eventType)
        {
            return (dispatchers[eventType]);
        }

        ///// <summary>
        ///// Get dispathcer by T
        ///// </summary>
        ///// <typeparam name="EventType">Type</typeparam>
        ///// <returns></returns>
        //public Dispatcher<Type> GetDispatcher<EventType>()
        //{
        //    return (dispatchers[typeof(EventType)]);
        //}

        ///// <summary>
        ///// Return a dispathcer is or not exist
        ///// </summary>
        ///// <typeparam name="EventType"></typeparam>
        ///// <returns></returns>
        //public bool ExistDispatcher<EventType>()
        //{
        //    return dispatchers.ContainsKey(typeof(EventType));
        //}

        /// <summary>
        /// Return a dispathcer is or not exit
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        public bool ExistDispatcher(TKey eventType)
        {
            return dispatchers.ContainsKey(eventType);
        }

        ///// <summary>
        ///// Fire event with async call
        ///// </summary>
        ///// <typeparam name="EventType">Dispathcer</typeparam>
        ///// <typeparam name="Event">Event</typeparam>
        ///// <param name="event">Event instance</param>
        //internal void RaiseEventAsync<EventType, Event>(Event @event) where Event : IEvent
        //{
        //    RaiseEventAsync(typeof(EventType), @event);
        //}

        /// <summary>
        /// Fire event with async call
        /// </summary>
        /// <typeparam name="Event">Event</typeparam>
        /// <param name="eventType">dispatcher</param>
        /// <param name="event">event instance</param>
        internal void RaiseEventAsync<Event>(TKey eventType, Event @event) where Event : IEvent
        {
            if (!GetDispatcher(eventType).ContainsKey(typeof(Event))) return;
            foreach (var item in GetDispatcher(eventType)[typeof(Event)])
            {
                var p = (EventHandlerFunc<Event>)item;
                Task.Run(() => p(@event));
            }

        }

        ///// <summary>
        ///// Fire event with sync call
        ///// </summary>
        ///// <typeparam name="EventType">Event dispathcer</typeparam>
        ///// <typeparam name="Event">Event</typeparam>
        ///// <param name="event">Event instance</param>
        //internal void RaiseEvent<EventType, Event>(Event @event) where Event : IEvent
        //{
        //    RaiseEvent(typeof(EventType), @event);
        //}

        /// <summary>
        /// Fire event with sync call
        /// </summary>
        /// <typeparam name="Event">Event</typeparam>
        /// <param name="eventType">dispatcher</param>
        /// <param name="event">event instance</param>
        internal void RaiseEvent<Event>(TKey eventType, Event @event) where Event : IEvent
        {
            Type typo = typeof(Event);
            if (!GetDispatcher(eventType).ContainsKey(typo)) return;
            foreach (var item in GetDispatcher(eventType)[typo])
            {
                ((EventHandlerFunc<Event>)item)(@event);
            }
        }

        /// <summary>
        /// Register event handler
        /// </summary>
        /// <typeparam name="Event">Target event</typeparam>
        /// <param name="eventType">Dispatcher</param>
        /// <param name="handler">handler</param>
        /// <returns></returns>
        public bool RegisterEventHandler<Event>(TKey eventType, EventHandlerFunc<Event> handler) where Event : IEvent
        {
            Type typo = typeof(Event);
            Dispatcher<Type> dispatcher = null;
            if (ExistDispatcher(eventType))
            {
                dispatcher = GetDispatcher(eventType);
            }
            else
            {
                throw new Exception("Dispatcher not register!");
            }

            if (!dispatcher.ContainsKey(typo))
            {
                dispatcher.Add(typo, new HandlerList());
            }
            if (dispatcher[typo].Contains(handler)) return false;
            dispatcher[typo].AddLast(handler);
            return true;
        }

        ///// <summary>
        ///// Register event handle
        ///// </summary>
        ///// <typeparam name="EventType">Event dispathcer</typeparam>
        ///// <typeparam name="Event">Target Event</typeparam>
        ///// <param name="handler">Handler</param>
        ///// <returns></returns>
        //public bool RegisterEventHandler<EventType, Event>(EventHandlerFunc<Event> handler) where Event : IEvent
        //{
        //    return RegisterEventHandler(typeof(EventType), handler);
        //}

        /// <summary>
        /// Remove event handle
        /// </summary>
        /// <typeparam name="EventType">Event dispathcer</typeparam>
        /// <typeparam name="Event">Target Event</typeparam>
        /// <param name="handler">Handler</param>
        public void RemoveEventHandler<Event>(TKey eventType, EventHandlerFunc<Event> handler) where Event : IEvent
        {
            if (dispatchers.TryGetValue(eventType, out Dispatcher<Type> dispatcher))
            {
                var list = dispatcher[typeof(Event)];
                list.Remove(handler);
            }
        }

        public void RemoveDispatcher(TKey eventType)
        {
            dispatchers.Remove(eventType);
        }
    }

}

using BoardCore.ServerCore.Lobby;
using BoardCore.ServerCore.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardCore.GameCore
{
    /// <summary>
    /// Implement this interface to tranform game data between two <see cref="Game"/> instance
    /// <para><see cref="IGamePlayerTransformer"/> transform data to <see cref="IGamePlayerReceiver"/></para>
    /// </summary>
    public interface IGamePlayerTransformer
    {
        IEnumerable<Player> GetPlayers();
    }

    /// <summary>
    /// Implement this interface to receive game data from other <see cref="Game"/> instance
    /// </summary>
    public interface IGamePlayerReceiver
    {
        void PeekPlayer(IEnumerable<Player> players);
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GameInfoAttribute: Attribute
    {
        public string Author { get; set; }
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public string Version { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GamePlayerAttribute : Attribute
    {
        public int MaxPlayer { get; set; }
        public int MinPlayer { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class GameGuidAttribute : Attribute
    {
        public string GUID { get; set; }
    }

    public abstract class Game
    {

        public abstract void IntializateNetworkRoom(Room room);
        public abstract void OnPlayerJoin(Player player);
        public abstract void OnPlayerLeave(Player player);


        public Game() { }
    }

    /// <summary>
    /// A base class for all <see cref="Game"/>
    /// </summary>
    public abstract class Game<T> : Game
        where T : Game<T>
    {
        protected Rule NextRule { get; private set; }
        public bool IsGameStart { get; protected set; }
        public bool IsGameEnd { get; protected set; }
        public static string Name { get; private set; }
        public static string Author { get; private set; }

        protected static readonly Dictionary<Type, Func<T, Rule>> RuleSet = new Dictionary<Type, Func<T, Rule>>();
        protected readonly Type CurrentGame = typeof(T);
        public readonly static GameInfo GameInfo = GameInfo.FromGameType(typeof(T));

        public Game()
        {
        }
        
        /// <summary>
        /// Run game
        /// </summary>
        public abstract Task OnGameAsync();

        public Rule ReplaceNextBehavior(Rule nextBehavior)
        {
            var oldBehavior = this.NextRule;
            NextRule = nextBehavior;
            return oldBehavior;
        }

        public static void RegisterRule<R>(Func<T, Rule<T, R>> ctor)
            where R : Rule<T, R>
        {
            RuleSet.Add(typeof(R), ctor);
        }

        public abstract void GameStart();
        public abstract void GameEnd();

        
        public Rule<T, R> DrawRule<R>() where R : Rule<T, R>
        {
            return RuleSet[typeof(R)]((T)this).ToRule<T, R>();
        }
    }
}

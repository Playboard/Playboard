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

    public abstract class Game
    {
        public string Name { get; private set; }
        public string Author { get; private set; }
        public int MaxPlayer { get; private set; }
        public int MinPalyer { get; private set; }

        public abstract void IntializateNetworkRoom(Room room);
        public abstract void OnPlayerJoin(Player player);
        public abstract void OnPlayerLeave(Player player);

        public Game(string Name, string Author, int MaxPlayer, int MinPlayer)
        {
            this.Name = Name;
            this.Author = Author;
            this.MaxPlayer = MaxPlayer;
            this.MinPalyer = MinPalyer;
        }
    }

    /// <summary>
    /// A base class for all <see cref="Game"/>
    /// </summary>
    public abstract class Game<T> : Game
        where T : Game<T> 
    {
        protected static readonly Dictionary<Type, Func<T, Rule>> RuleSet = new Dictionary<Type, Func<T, Rule>>();
        protected readonly Type CurrentGame = typeof(T);

        public Game(string Name, string Author, int MaxPlayer, int MinPlayer) : base(Name, Author, MaxPlayer, MinPlayer) { }

        protected Rule NextRule { get; private set; }
        public bool IsGameStart { get; protected set; }
        public bool IsGameEnd { get; protected set; }

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

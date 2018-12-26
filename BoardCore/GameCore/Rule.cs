using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardCore.GameCore
{
    public abstract class Rule
    {
        public Rule<GameImpl, T> ToRule<GameImpl, T>()
            where GameImpl : Game<GameImpl>
            where T : Rule<GameImpl, T>
        {
            return this as Rule<GameImpl, T>;
        }

    }

    /// <summary>
    /// A behavior in game for a type
    /// </summary>
    /// <typeparam name="GameImpl">Game</typeparam>
    /// <typeparam name="T"></typeparam>
    public abstract class Rule<GameImpl, T> : Rule
        where GameImpl : Game<GameImpl>
        where T : Rule<GameImpl, T>
    {
        protected GameImpl Game { get; private set; }
        public Rule(GameImpl game)
        {
            this.Game = game;
        }
        public virtual void AfterRule() { }
        public virtual void BeforeRule() { }
        public abstract Task<Rule> OnBehaviorAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BoardCore.GameCore.GameCard
{
    public abstract class Card
    {
        public Card<GameImpl, T> ToCard<GameImpl, T>()
            where GameImpl : Game<GameImpl>
            where T : Card<GameImpl, T>
        {
            return this as Card<GameImpl, T>;
        }

    }

    public abstract class Card<GameImpl, T> : Card
        where GameImpl : Game<GameImpl>
        where T : Card<GameImpl, T>
    {
        protected readonly GameImpl Game;
        public string CardName { get; protected set; }
        public Guid Guid { get; protected set; }

        public Card(GameImpl Game, string CardName)
        {
            this.Game = Game;
            this.CardName = CardName;
            this.Guid = Guid.NewGuid();
        }
    }
}

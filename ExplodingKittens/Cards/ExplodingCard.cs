using BoardCore.GameCore.GameCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExplodingKittens.Cards
{
    public class ExplodingCard<T> : Card<ExplodingKittens, T>
        where T : ExplodingCard<T>
    {
        public ExplodingCard(ExplodingKittens Game, string CardName) : base(Game, CardName)
        {
        }
    }
}

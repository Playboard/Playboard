using BoardCore.GameCore.GameCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExplodingKittens.Cards
{
    public class NopeCard : ExplodingCard<NopeCard>
    {
        public NopeCard(ExplodingKittens Game) : base(Game, "Nope")
        {
        }
    }
}

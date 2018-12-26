using BoardCore.GameCore.GameCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExplodingKittens.Cards
{
    public class SkipCard : ExplodingCard<SkipCard>
    {
        public SkipCard(ExplodingKittens Game) : base(Game, "Skip")
        {
        }
    }
}

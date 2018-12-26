using BoardCore.GameCore.GameCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExplodingKittens.Cards
{
    public class ShuffleCard : ExplodingCard<ShuffleCard>
    {
        public ShuffleCard(ExplodingKittens Game) : base(Game, "Shuffle")
        {
        }
    }
}

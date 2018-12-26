using BoardCore.GameCore.GameCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExplodingKittens.Cards
{
    public class FavorCard : ExplodingCard<FavorCard>
    {
        public FavorCard(ExplodingKittens Game) : base(Game, "Favor")
        {
        }
    }
}

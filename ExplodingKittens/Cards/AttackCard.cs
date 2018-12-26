using BoardCore.GameCore.GameCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExplodingKittens.Cards
{
    public class AttackCard : ExplodingCard<AttackCard>
    {
        public AttackCard(ExplodingKittens Game) : base(Game, "Attack")
        {
        }
    }
}

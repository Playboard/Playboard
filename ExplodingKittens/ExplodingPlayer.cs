using BoardCore.GameCore;
using BoardCore.GameCore.GameCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExplodingKittens
{
    public class ExplodingPlayer : GamePlayer<ExplodingKittens, ExplodingPlayer>
    {
        public bool Dead { get; set; } = false;
        public ExplodingPlayer(ExplodingKittens Game, string PlayerName) : base(Game, PlayerName)
        {
        }
        
    }
}

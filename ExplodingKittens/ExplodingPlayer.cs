using BoardCore.GameCore;
using BoardCore.GameCore.GameCard;
using BoardCore.ServerCore.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExplodingKittens
{
    public class ExplodingPlayer : GamePlayer<ExplodingKittens, ExplodingPlayer>
    {
        public bool Dead { get; set; } = false;
        public ExplodingPlayer(ExplodingKittens Game, Player Player) : base(Game, Player)
        {
        }
        
    }
}

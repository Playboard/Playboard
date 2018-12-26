using BoardCore.GameCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExplodingKittens.Rules
{
    public class PlayerDrawNope : Rule<ExplodingKittens, PlayerDrawNope>
    {
        public PlayerDrawNope(ExplodingKittens game) : base(game)
        {
        }

        public override Task<Rule> OnBehaviorAsync()
        {
            throw new NotImplementedException();
        }
    }
}

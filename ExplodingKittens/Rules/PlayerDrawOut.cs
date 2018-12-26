using BoardCore.GameCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExplodingKittens.Rules
{
    public class PlayerDrawOut : Rule<ExplodingKittens, PlayerDrawOut>
    {
        public PlayerDrawOut(ExplodingKittens game) : base(game)
        {
        }

        public override Task<Rule> OnBehaviorAsync()
        {
            throw new NotImplementedException();
        }
    }
}

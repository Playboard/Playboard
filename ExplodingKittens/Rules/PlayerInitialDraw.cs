using BoardCore.GameCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExplodingKittens.Rules
{
    public class PlayerInitialDraw : Rule<ExplodingKittens, PlayerInitialDraw>
    {
        public PlayerInitialDraw(ExplodingKittens game) : base(game) { }

        public override Task<Rule> OnBehaviorAsync()
        {
            throw new NotImplementedException();
        }
    }
}

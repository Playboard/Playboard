using BoardCore.GameCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExplodingKittens.Rules
{
    public class PlayerWin : Rule<ExplodingKittens, PlayerWin>
    {
        public PlayerWin(ExplodingKittens game) : base(game)
        {
        }

        public override Task<Rule> OnBehaviorAsync()
        {
            throw new NotImplementedException();
        }
    }
}

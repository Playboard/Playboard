using BoardCore.GameCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ExplodingKittens.Rules
{
    public class WaitingPlayer : Rule<ExplodingKittens, WaitingPlayer>
    {
        public WaitingPlayer(ExplodingKittens explodingKittens) : base(explodingKittens) { }

        public override async Task<Rule> OnBehaviorAsync()
        {
            //等待所有玩家准备
            while (Game.Players.Any(p => !p.ReadyStatus))
            {
                await Task.Delay(1);
            }
            Game.GameStart();
            //接下来分配卡池随机抽卡
            return Game.DrawRule<PlayerInitialDraw>();
        }
    }
}

using BoardCore.GameCore;
using BoardCore.GameCore.GameCard;
using BoardCore.GameCore.Utils;
using BoardCore.ServerCore.Lobby;
using BoardCore.ServerCore.Network;
using ExplodingKittens.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExplodingKittens
{
    [GameInfo(Author = "Deliay", Name = "Exploding Kittens", FriendlyName = "爆炸猫咪", Version = "1.0")]
    [GamePlayer(MinPlayer = 2, MaxPlayer = 5)]
    [GameGuid(GUID = "632C8AF3-4FF1-4539-8144-FF3DA7E87771")]
    public class ExplodingKittens : Game<ExplodingKittens>
    {
        public LinkedList<ExplodingPlayer> Players = new LinkedList<ExplodingPlayer>();
        public ObjectEnumerator<ExplodingPlayer> PlayerEnumerator;
        public ExplodingPlayer CurrentPlayer = null;
        public ExplodingPlayer NextPlayer = null;
        public CardSet<Guid> CardSet = new CardSet<Guid>();
        static ExplodingKittens()
        {
            RegisterRule(game => new WaitingPlayer(game));
            RegisterRule(game => new PlayerInitialDraw(game));
            RegisterRule(game => new PlayerDraw(game));
            RegisterRule(game => new PlayerDrawOut(game));
            RegisterRule(game => new PlayerDrawNope(game));
            RegisterRule(game => new PlayerWin(game));
        }

        public ExplodingKittens() { }

        public override async Task OnGameAsync()
        {
            // 等待玩家进入，并给玩家发牌
            var playerInitial = await DrawRule<WaitingPlayer>().OnBehaviorAsync() as PlayerInitialDraw;
            // 等抽卡完成
            var curr = await playerInitial.OnBehaviorAsync();
            // 初始化玩家转盘
            PlayerEnumerator = new ObjectEnumerator<ExplodingPlayer>(Players);
            // 使用随机玩家开始
            bool funcPlayerNotDead(ExplodingPlayer x) => !x.Dead;
            CurrentPlayer = PlayerEnumerator.GetRandomInitial();
            NextPlayer = PlayerEnumerator.NextWhen(funcPlayerNotDead);

            while (curr != null && CurrentPlayer != NextPlayer)
            {
                // 当前玩家的一轮
                curr = await RunGameRule(curr);
                // 交换玩家，直到下一玩家=当前玩家
                CurrentPlayer = NextPlayer;
                NextPlayer = PlayerEnumerator.NextWhen(funcPlayerNotDead);
            }
            // 判定游戏不能继续了
            if (CurrentPlayer == NextPlayer)
            {
                await DrawRule<PlayerWin>().OnBehaviorAsync(); // 玩家获胜
                GameEnd();
            }
            else throw new Exception(""); // 出BUG了
        }

        public async Task<Rule> RunGameRule(Rule rule)
        {
            // 所有玩家可选操作超时30秒，不选则为放弃
            // 拆弹则会直接自动打出，不会设置超时。
            switch (rule)
            {
                case null: break;
                case PlayerDrawOut drawOut: //出牌阶段
                    // 玩家出一张牌，如果仍然可以出牌，继续返回PlayerDrawOut
                    Rule next = await drawOut.OnBehaviorAsync();
                    // 直到不能出牌或选择跳过为止
                    while (next is PlayerDrawOut nextDraw)
                    {
                        next = await nextDraw.OnBehaviorAsync();
                    }
                    return await RunGameRule(next); // 进行下一阶段
                case PlayerDraw draw: // 抽卡阶段
                    // 在抽卡时判断该玩家是否死亡
                    return await draw.OnBehaviorAsync();   
            }
            return null;
        }

        //public Rule<ExplodingKittens, T> DrawRule<T>() where T : Rule<ExplodingKittens, T>
        //{
        //    return RuleSet[typeof(T)](this).ToRule<ExplodingKittens, T>();
        //}

        public override void GameStart()
        {
            if (this.Players.Count() > GameInfo.MaxPlayer)
            {
                base.IsGameStart = true;
            }
        }

        public override void GameEnd()
        {
            throw new NotImplementedException();
        }

        public override void IntializateNetworkRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public override void OnPlayerJoin(Player player)
        {
            throw new NotImplementedException();
        }

        public override void OnPlayerLeave(Player player)
        {
            throw new NotImplementedException();
        }
    }
}

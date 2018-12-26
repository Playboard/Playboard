using BoardCore.GameCore.GameCard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExplodingKittens.Cards
{
    public class ExplodingNekoCard : ExplodingCard<ExplodingNekoCard>
    {
        public ExplodingNekoCard(ExplodingKittens Game) : base(Game, "Exploding Neko")
        {
        }
    }

    public class RainbowNekoCard : ExplodingCard<RainbowNekoCard>
    {
        public RainbowNekoCard(ExplodingKittens Game) : base(Game, "Rainbow Neko")
        {
        }
    }

    public class TomatoNekoCard : ExplodingCard<TomatoNekoCard>
    {
        public TomatoNekoCard(ExplodingKittens Game) : base(Game, "Tomato Neko")
        {
        }
    }

    public class MelonNekoCard : ExplodingCard<MelonNekoCard>
    {
        public MelonNekoCard(ExplodingKittens Game) : base(Game, "Melon Neko")
        {

        }
    }

    public class BeardNekoCard : ExplodingCard<BeardNekoCard>
    {
        public BeardNekoCard(ExplodingKittens Game) : base(Game, "Beard Neko")
        {
        }
    }

    public class TaccoNekoCard : ExplodingCard<TaccoNekoCard>
    {
        public TaccoNekoCard(ExplodingKittens Game) : base(Game, "Tacco Neko")
        {
        }
    }
}

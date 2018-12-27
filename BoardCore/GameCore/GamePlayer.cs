using BoardCore.GameCore.GameCard;
using BoardCore.ServerCore.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoardCore.GameCore
{
    public abstract class GamePlayer
    {
        public GamePlayer<GameImpl, T> ToPlayer<GameImpl, T>()
            where T : GamePlayer<GameImpl, T>
            where GameImpl : Game<GameImpl>
        {
            return this as GamePlayer<GameImpl, T>;
        }
    }

    public abstract class GamePlayer<GameImpl, T>
        where T : GamePlayer<GameImpl, T>
        where GameImpl : Game<GameImpl>
    {
        public GameImpl Game { get; private set; }
        public Player NetworkPlayer { get; private set; }
        public virtual string PlayerName { get; protected set; }
        public bool ReadyStatus { get; set; }
        public GamePlayer(GameImpl Game, Player networkPlayer)
        {
            this.NetworkPlayer = NetworkPlayer;
            this.PlayerName = NetworkPlayer.Name;
            this.Game = Game;
        }
    }
}

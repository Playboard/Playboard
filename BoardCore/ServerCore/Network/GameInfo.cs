using BoardCore.GameCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BoardCore.ServerCore.Network
{
    public struct GameInfo
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string FriendlyName { get; set; }
        public string GUID { get; set; }
        public int MaxPlayer { get; set; }
        public int MinPlayer { get; set; }

        public static GameInfo FromGameType(Type type)
        {
            var info = type.GetCustomAttribute<GameInfoAttribute>();
            var player = type.GetCustomAttribute<GamePlayerAttribute>();
            var guid = type.GetCustomAttribute<GameGuidAttribute>();
            return new GameInfo
            {
                Author = info.Author, Name = info.Name,
                FriendlyName = info.FriendlyName,
                MaxPlayer = player.MaxPlayer, MinPlayer = player.MinPlayer,
                GUID = guid.GUID,
            };
        }
    }
}

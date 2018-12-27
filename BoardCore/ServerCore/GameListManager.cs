using BoardCore.GameCore;
using BoardCore.ServerCore.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BoardCore
{
    public class GameListManager
    {
        public readonly static GameListManager Instance = new GameListManager();
        public IReadOnlyDictionary<string, GameInfo> GameInfo;
        public IReadOnlyDictionary<string, Func<Game>> GameGenerator;

        private GameListManager()
        {
            var allFiles = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");

            LinkedList<Assembly> assemblies = new LinkedList<Assembly>();
            foreach (var file in allFiles)
            {
                try
                {
                    if (assemblies.Any(f => f.Location == file)) continue;
                    assemblies.AddLast(Assembly.LoadFrom(file));
                }
                catch
                {
                    continue;
                }
            }

            // 游戏之间假定无任何依赖关系
            // 直接并行load

            // .Net 会自己优化parallel，无需我们手动设置concurrency
            var gameType = typeof(Game);
            var check = new Func<Type, bool>[] {
                asm => asm.IsClass && asm.IsPublic,
                asm => gameType.IsAssignableFrom(asm),
                asm => asm.GetCustomAttribute<GameInfoAttribute>() != null && asm.GetCustomAttribute<GameGuidAttribute>() != null,
            };

            var gameInfo = new Dictionary<string, GameInfo>();
            var gameGenerator = new Dictionary<string, Func<Game>>();

            Parallel.ForEach(assemblies, (assembly) => {
                foreach (var type in assembly.ExportedTypes)
                {
                    if (!check.All(x => x(type))) continue;
                    var info = ServerCore.Network.GameInfo.FromGameType(type);
                    gameInfo.Add(info.GUID, info);
                    gameGenerator.Add(info.GUID, () => assembly.CreateInstance(type.FullName) as Game);
                }
            });

            GameInfo = gameInfo;
            GameGenerator = gameGenerator;
        }
    }
}

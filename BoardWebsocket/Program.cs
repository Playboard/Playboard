using BoardCore;
using System;

namespace BoardServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading games...");
            foreach (var (Key, Value) in GameListManager.Instance.GameInfo)
            {
                Console.WriteLine($"{Value.FriendlyName}({Value.Author}) [{Value.MinPlayer}-{Value.MaxPlayer}人] {Value.Name} {Value.GUID}");
            }
            // 主线程留给控制台输入
            // 另起一个线程跑服务器
        }
    }
}

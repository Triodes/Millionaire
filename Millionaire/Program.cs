using System;

namespace Millionaire
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Millionaire())
                game.Run();
        }
    }
}

using osu.Framework;
using osu.Framework.Platform;
using touhou.sharp.Game;

namespace THSharp.Desktop
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            using (GameHost host = Host.GetSuitableHost("THSharp"))
            {
                host.Run(new THSharpGame());
            }
        }
    }
}

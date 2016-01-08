using System;

namespace Timeravel
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Timeravel game = new Timeravel())
            {
                game.Run();
            }
        }
    }
#endif
}


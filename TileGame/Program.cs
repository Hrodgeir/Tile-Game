namespace TileGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TileGame game = new TileGame())
            {
                game.Run();
            }
        }
    }
#endif
}


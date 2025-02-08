namespace MinecraftCommandAPI.Models
{
    public class GamemodeCommand
    {
        public string Mode { get; set; }

        public GamemodeCommand(string mode)
        {
            Mode = mode;
        }

        public static GamemodeCommand Creative() => new GamemodeCommand("creative");
        public static GamemodeCommand Survival() => new GamemodeCommand("survival");
        public static GamemodeCommand Adventure() => new GamemodeCommand("adventure");
        public static GamemodeCommand Spectator() => new GamemodeCommand("spectator");
    }
}

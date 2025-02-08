namespace MinecraftCommandAPI.Models
{
    public class CommandTarget
    {
        public string Target { get; set; }
        public string PlayerName { get; set; }

        public CommandTarget(string target, string playerName = null)
        {
            Target = target;
            PlayerName = playerName;
        }

        public static CommandTarget Self() => new CommandTarget("@s");
        public static CommandTarget NearestPlayer() => new CommandTarget("@p");
        public static CommandTarget AllPlayers() => new CommandTarget("@a");
        public static CommandTarget RandomPlayer() => new CommandTarget("@r");
        public static CommandTarget Player(string playerName) => new CommandTarget(playerName);
    }
}

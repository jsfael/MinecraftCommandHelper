using MinecraftCommandAPI.Models;

namespace MinecraftCommandHelper.Model
{
    public class TeleportCommand
    {
        public CommandTarget Source { get; set; }
        public CommandTarget Destination { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Z { get; set; }

        public TeleportCommand(CommandTarget source, double x, double y, double z)
        {
            Source = source;
            X = x;
            Y = y;
            Z = z;
        }

        public TeleportCommand(CommandTarget source, CommandTarget destination)
        {
            Source = source;
            Destination = destination;
        }

        public override string ToString()
        {
            if (Destination != null)
            {
                return $"/tp {Source.Target} {Destination.Target}";
            }
            else if (X.HasValue && Y.HasValue && Z.HasValue)
            {
                return $"/tp {Source.Target} {X} {Y} {Z}";
            }
            else
            {
                throw new InvalidOperationException("Invalid teleport command configuration.");
            }
        }
    }
}

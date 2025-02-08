using MinecraftCommandAPI.Models;

namespace MinecraftCommandHelper.Model
{
    public class EffectCommand
    {
        public CommandTarget Target { get; set; }
        public string Effect { get; set; }
        public int? Duration { get; set; }
        public int? Amplifier { get; set; }
        public bool? HideParticles { get; set; }

        public EffectCommand(CommandTarget target, string effect, int? duration = null, int? amplifier = null, bool? hideParticles = null)
        {
            Target = target;
            Effect = effect;
            Duration = duration;
            Amplifier = amplifier;
            HideParticles = hideParticles;
        }

        public override string ToString()
        {
            var command = $"/effect {Target.Target} {Effect}";

            if (Duration.HasValue)
            {
                command += $" {Duration.Value}";
            }

            if (Amplifier.HasValue)
            {
                command += $" {Amplifier.Value}";
            }

            if (HideParticles.HasValue)
            {
                command += $" {HideParticles.Value.ToString().ToLower()}";
            }

            return command;
        }
    }
}

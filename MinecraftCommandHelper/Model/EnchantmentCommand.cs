using MinecraftCommandAPI.Models;

namespace MinecraftCommandHelper.Model
{
    public class EnchantmentCommand
    {
        public CommandTarget Target { get; set; }
        public string Enchantment { get; set; }
        public int Level { get; set; }

        public EnchantmentCommand(CommandTarget target, string enchantment, int level)
        {
            Target = target;
            Enchantment = enchantment;
            Level = level;
        }

        public override string ToString()
        {
            return $"/enchant {Target.Target} {Enchantment} {Level}";
        }
    }
}

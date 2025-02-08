using Microsoft.AspNetCore.Mvc;
using MinecraftCommandAPI.Models;
using MinecraftCommandHelper.Model;
using System.IO;
using System.Text.Json;

namespace MinecraftCommandAPI.Controllers
{
    [ApiController]
    [Route("api/enchant")]
    public class EnchantmentController : ControllerBase
    {
        private static readonly Dictionary<string, int[]> MinecraftEnchant;

        static EnchantmentController()
        {
            var json = System.IO.File.ReadAllText("GameData/Content/minecraft_enchantment.json");
            MinecraftEnchant = JsonSerializer.Deserialize<Dictionary<string, int[]>>(json) ?? new Dictionary<string, int[]>();
        }

        [HttpGet("give")]
        public IActionResult GetEnchantmentCommand([FromQuery] string? target, [FromQuery] string? playerName, [FromQuery] string? enchantment, [FromQuery] int? level = null)
        {
            if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(playerName))
            {
                return BadRequest("Specify either target or playerName, not both.");
            }

            if (string.IsNullOrEmpty(target) && string.IsNullOrEmpty(playerName))
            {
                return BadRequest("Either target or playerName must be specified.");
            }

            if (string.IsNullOrEmpty(enchantment))
            {
                return Ok(new { AvailableEnchantments = MinecraftEnchant.Keys });
            }

            if (!MinecraftEnchant.ContainsKey(enchantment))
            {
                return BadRequest("Invalid enchantment specified.");
            }

            if (level == null || !MinecraftEnchant[enchantment].Contains(level.Value))
            {
                return BadRequest("Invalid level specified for the given enchantment.");
            }

            CommandTarget commandTarget;

            if (!string.IsNullOrEmpty(playerName))
            {
                commandTarget = CommandTarget.Player(playerName);
            }
            else
            {
                switch (target.ToLower())
                {
                    case "self":
                        commandTarget = CommandTarget.Self();
                        break;
                    case "nearest":
                        commandTarget = CommandTarget.NearestPlayer();
                        break;
                    case "all":
                        commandTarget = CommandTarget.AllPlayers();
                        break;
                    case "random":
                        commandTarget = CommandTarget.RandomPlayer();
                        break;
                    default:
                        return BadRequest("Invalid target specified.");
                }
            }

            var enchantmentCommand = new EnchantmentCommand(commandTarget, enchantment, level.Value);
            var command = enchantmentCommand.ToString();
            return Ok(new { Command = command });
        }
    }
}

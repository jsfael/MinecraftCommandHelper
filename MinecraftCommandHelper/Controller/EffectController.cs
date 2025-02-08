using Microsoft.AspNetCore.Mvc;
using MinecraftCommandAPI.Models;
using MinecraftCommandHelper.Model;
using System.IO;
using System.Text.Json;

namespace MinecraftCommandAPI.Controllers
{
    [ApiController]
    [Route("api/effect")]
    public class EffectController : ControllerBase
    {
        private static readonly string[] MinecraftEffects;

        static EffectController()
        {
            var json = System.IO.File.ReadAllText("GameData/Content/minecraft_effects.json");
            MinecraftEffects = JsonSerializer.Deserialize<string[]>(json);
        }

        [HttpGet("give")]
        public IActionResult GetEffectCommand([FromQuery] string? target, [FromQuery] string? playerName, [FromQuery] string? effect, [FromQuery] int? duration = null, [FromQuery] int? amplifier = null, [FromQuery] bool? hideParticles = null)
        {
            if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(playerName))
            {
                return BadRequest("Specify either target or playerName, not both.");
            }

            if (string.IsNullOrEmpty(target) && string.IsNullOrEmpty(playerName))
            {
                return BadRequest("Either target or playerName must be specified.");
            }

            if (string.IsNullOrEmpty(effect))
            {
                return Ok(new { AvailableEffects = MinecraftEffects });
            }

            if (!MinecraftEffects.Contains(effect))
            {
                return BadRequest("Invalid effect specified.");
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

            var effectCommand = new EffectCommand(commandTarget, effect, duration, amplifier, hideParticles);
            var command = effectCommand.ToString();
            return Ok(new { Command = command });
        }
    }
}

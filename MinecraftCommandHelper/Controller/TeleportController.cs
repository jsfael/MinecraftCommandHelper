using Microsoft.AspNetCore.Mvc;
using MinecraftCommandAPI.Models;
using MinecraftCommandHelper.Model;

namespace MinecraftCommandHelper.Controller
{
    [ApiController]
    [Route("api/teleport")]
    public class TeleportController : ControllerBase
    {
        [HttpGet("coordinates")]
        public IActionResult TeleportToCoordinates([FromQuery] string? playerName, [FromQuery] string? source, [FromQuery] double x, [FromQuery] double y, [FromQuery] double z)
        {
            if (!string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(playerName))
            {
                return BadRequest("Specify either source or playerName, not both.");
            }

            CommandTarget commandSource;

            if (!string.IsNullOrEmpty(playerName))
            {
                commandSource = CommandTarget.Player(playerName);
            }
            else if (!string.IsNullOrEmpty(source))
            {
                switch (source.ToLower())
                {
                    case "self":
                        commandSource = CommandTarget.Self();
                        break;
                    case "nearest":
                        commandSource = CommandTarget.NearestPlayer();
                        break;
                    case "all":
                        commandSource = CommandTarget.AllPlayers();
                        break;
                    case "random":
                        commandSource = CommandTarget.RandomPlayer();
                        break;
                    default:
                        return BadRequest("Invalid source value.");
                }
            }
            else
            {
                return BadRequest("Either source or playerName must be specified.");
            }

            var teleportCommand = new TeleportCommand(commandSource, x, y, z);
            var command = teleportCommand.ToString();
            return Ok(new { Command = command });
        }
    }
}

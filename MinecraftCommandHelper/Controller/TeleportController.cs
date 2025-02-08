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
        public IActionResult TeleportToCoordinates([FromQuery] string? source, [FromQuery] double x, [FromQuery] double y, [FromQuery] double z, [FromQuery] string? playerName)
        {
            if (!string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(playerName))
            {
                return BadRequest("Specify either source or playerName, not both.");
            }

            CommandTarget commandSource;

            // Verificar se playerName foi fornecido
            if (!string.IsNullOrEmpty(playerName))
            {
                commandSource = CommandTarget.Player(playerName);
            }
            else if (!string.IsNullOrEmpty(source))
            {
                // Verificar o tipo de source escolhido
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

        [HttpGet("target")]
        public IActionResult TeleportToTarget([FromQuery] string source, [FromQuery] string destination)
        {
            CommandTarget commandSource;
            CommandTarget commandDestination;

            // Verificar o tipo de source escolhido
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

            // Verificar o tipo de destination escolhido
            switch (destination.ToLower())
            {
                case "self":
                    commandDestination = CommandTarget.Self();
                    break;
                case "nearest":
                    commandDestination = CommandTarget.NearestPlayer();
                    break;
                case "all":
                    commandDestination = CommandTarget.AllPlayers();
                    break;
                case "random":
                    commandDestination = CommandTarget.RandomPlayer();
                    break;
                default:
                    commandDestination = CommandTarget.Player(destination);
                    break;
            }

            var teleportCommand = new TeleportCommand(commandSource, commandDestination);
            var command = teleportCommand.ToString();
            return Ok(new { Command = command });
        }
    }
}



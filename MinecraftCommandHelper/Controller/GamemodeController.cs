using Microsoft.AspNetCore.Mvc;
using MinecraftCommandAPI.Models;

namespace MinecraftCommandAPI.Controllers
{
    [ApiController]
    [Route("api/gamemode")]
    public class GamemodeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetGamemodeCommand([FromQuery] string gamemode, [FromQuery] string target = null, [FromQuery] string playerName = null)
        {
            if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(playerName))
            {
                return BadRequest("Specify either target or playerName, not both.");
            }

            CommandTarget commandTarget;
            GamemodeCommand commandGamemode;

            // Verificar se playerName foi fornecido
            if (!string.IsNullOrEmpty(playerName))
            {
                commandTarget = CommandTarget.Player(playerName);
            }
            else if (!string.IsNullOrEmpty(target))
            {
                // Verificar o tipo de target escolhido
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
            else
            {
                return BadRequest("Either target or playerName must be specified.");
            }

            // Verificar o tipo de gamemode escolhido
            switch (gamemode.ToLower())
            {
                case "creative":
                    commandGamemode = GamemodeCommand.Creative();
                    break;
                case "survival":
                    commandGamemode = GamemodeCommand.Survival();
                    break;
                case "adventure":
                    commandGamemode = GamemodeCommand.Adventure();
                    break;
                case "spectator":
                    commandGamemode = GamemodeCommand.Spectator();
                    break;
                default:
                    return BadRequest("Invalid gamemode specified.");
            }

            var command = $"/gamemode {commandGamemode.Mode} {commandTarget.Target}";
            return Ok(new { Command = command });
        }
    }
}

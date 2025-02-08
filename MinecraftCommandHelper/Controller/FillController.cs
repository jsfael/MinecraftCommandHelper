using Microsoft.AspNetCore.Mvc;
using MinecraftCommandAPI.Models;

[ApiController]
[Route("api/fill")]
public class FillController : ControllerBase
{
    [HttpGet]
    public IActionResult GetFillCommand([FromQuery] string? source, [FromQuery] string? playerName, [FromQuery] string block, [FromQuery] double width = 1, [FromQuery] double height = 1, [FromQuery] double depth = 1)
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

        var fillCommand = new FillCommand(commandSource, width, height, depth, block);
        var command = fillCommand.ToString();
        return Ok(new { Command = command });
    }
}

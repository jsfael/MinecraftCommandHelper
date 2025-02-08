using Microsoft.AspNetCore.Mvc;
using MinecraftCommandAPI.Models;
using MinecraftCommandHelper.Model;
using System.IO;
using System.Text.Json;

namespace MinecraftCommandAPI.Controllers
{
    [ApiController]
    [Route("api/give")]
    public class GiveItemsController : ControllerBase
    {
        private static readonly string[] MinecraftItems;

        static GiveItemsController()
        {
            var json = System.IO.File.ReadAllText("GameData/Items/minecraft_items.json");
            MinecraftItems = JsonSerializer.Deserialize<string[]>(json);
        }

        [HttpGet]
        public IActionResult GetGiveCommand([FromQuery] string? target, [FromQuery] string? playerName, [FromQuery] string item, [FromQuery] int quantity)
        {
            if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(playerName))
            {
                return BadRequest("Specify either target or playerName, not both.");
            }

            if (string.IsNullOrEmpty(target) && string.IsNullOrEmpty(playerName))
            {
                return BadRequest("Either target or playerName must be specified.");
            }

            if (string.IsNullOrEmpty(item) || quantity <= 0)
            {
                return BadRequest("Item and quantity must be specified and quantity must be greater than 0.");
            }

            if (!MinecraftItems.Contains(item))
            {
                return BadRequest("Invalid item specified.");
            }

            CommandTarget commandTarget;

            // Verificar se playerName foi fornecido
            if (!string.IsNullOrEmpty(playerName))
            {
                commandTarget = CommandTarget.Player(playerName);
            }
            else
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

            var giveItemsCommand = new GiveItemsCommand(commandTarget.Target, item, quantity);
            var command = giveItemsCommand.GenerateCommand();
            return Ok(new { Command = command });
        }
    }
}


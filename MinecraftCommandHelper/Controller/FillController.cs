using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MinecraftCommandAPI.Models;

namespace MinecraftCommandAPI.Controllers
{
    [ApiController]
    [Route("api/fill")]
    public class FillController : ControllerBase
    {
        private static readonly string[] MinecraftBlocks;

        static FillController()
        {
            var json = System.IO.File.ReadAllText("GameData/Content/minecraft_blocks_filtered.json");
            MinecraftBlocks = JsonSerializer.Deserialize<string[]>(json);
        }

        [HttpGet]
        public IActionResult GetFillCommand([FromQuery] string block, [FromQuery] double width = 1, [FromQuery] double height = 1, [FromQuery] double depth = 1, [FromQuery] string mode = null, [FromQuery] string oldBlock = null)
        {
            if (!MinecraftBlocks.Contains(block))
            {
                return BadRequest("Invalid block specified.");
            }

            if (mode != null && mode != "destroy" && mode != "keep" && mode != "outline" && mode != "hollow" && mode != "replace")
            {
                return BadRequest("Mode must be one of the following: destroy, keep, outline, hollow, replace.");
            }

            if (mode == "replace")
            {
                if (string.IsNullOrEmpty(oldBlock))
                {
                    return BadRequest("When mode is 'replace', oldBlock must be specified.");
                }

                if (!MinecraftBlocks.Contains(oldBlock))
                {
                    return BadRequest("Invalid oldBlock specified.");
                }
            }
            else if (!string.IsNullOrEmpty(oldBlock))
            {
                return BadRequest("OldBlock should not be specified when mode is not 'replace'.");
            }

            var fillCommand = new FillCommand(width, height, depth, block, mode, oldBlock);
            var command = fillCommand.ToString();
            return Ok(new { Command = command });
        }
    }
}

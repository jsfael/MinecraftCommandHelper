using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        public IActionResult GetFillCommand([FromQuery] string block, [FromQuery] double width = 1, [FromQuery] double height = 1, [FromQuery] double depth = 1)
        {
            if (string.IsNullOrEmpty(block) || width <= 0 || height <= 0 || depth <= 0)
            {
                return BadRequest("Block, width, height, and depth must be specified and greater than 0.");
            }

            if (!MinecraftBlocks.Contains(block))
            {
                return BadRequest("Invalid block specified.");
            }

            var command = $"/fill ~ ~ ~ ~+{width - 1} ~+{height - 1} ~+{depth - 1} minecraft:{block}";
            return Ok(new { Command = command });
        }
    }
}

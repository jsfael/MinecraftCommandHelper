using Microsoft.AspNetCore.Mvc;
using MinecraftCommandAPI.Models;
using MinecraftCommandHelper.Model;
using System.IO;
using System.Text.Json;

namespace MinecraftCommandAPI.Controllers
{
    [ApiController]
    [Route("api/entity")]
    public class EntityController : ControllerBase
    {
        private static readonly string[] MinecraftEntity;

        static EntityController()
        {
            var json = System.IO.File.ReadAllText("GameData/Content/minecraft_entities.json");
            MinecraftEntity = JsonSerializer.Deserialize<string[]>(json);
        }

        [HttpGet]
        public IActionResult SummonEntity([FromQuery] string entity)
        {
            if (string.IsNullOrEmpty(entity))
            {
                return BadRequest("Entity must be specified.");
            }

            if (!MinecraftEntity.Contains(entity))
            {
                return BadRequest("Invalid entity specified.");
            }

            var entitiesCommand = new EntitiesCommand(entity);
            var command = entitiesCommand.GenerateCommand();
            return Ok(new { Command = command });
        }
    }
}

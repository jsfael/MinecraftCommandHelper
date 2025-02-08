namespace MinecraftCommandHelper.Model
{
    public class EntitiesCommand
    {
        public string Entity { get; set; }

        public EntitiesCommand(string entity)
        {
            Entity = entity;
        }

        public string GenerateCommand()
        {
            return $"/summon {Entity}";
        }
    }
}

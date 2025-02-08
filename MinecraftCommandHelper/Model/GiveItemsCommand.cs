namespace MinecraftCommandHelper.Model
{
    public class GiveItemsCommand
    {
        public string Target { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }

        public GiveItemsCommand(string target, string item, int quantity)
        {
            Target = target;
            Item = item;
            Quantity = quantity;
        }

        public string GenerateCommand()
        {
            return $"/give {Target} minecraft:{Item} {Quantity}";
        }
    }
}

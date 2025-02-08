using MinecraftCommandAPI.Models;

public class FillCommand
{
    public CommandTarget Source { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public string Block { get; set; }

    public FillCommand(CommandTarget source, double width, double height, double depth, string block)
    {
        Source = source;
        Width = width;
        Height = height;
        Depth = depth;
        Block = block;
    }

    public string ToString()
    {
        return $"/fill {Source.Target} ~ ~ ~ ~+{Width - 1} ~+{Height - 1} ~+{Depth - 1} {Block}";
    }
}

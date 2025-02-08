using MinecraftCommandAPI.Models;

public class FillCommand
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public string Block { get; set; }

    public FillCommand( double width, double height, double depth, string block)
    {

        Width = width;
        Height = height;
        Depth = depth;
        Block = block;
    }

    public string ToString()
    {
        return $"/fill  ~ ~ ~ ~+{Width - 1} ~+{Height - 1} ~+{Depth - 1} minecraft:{Block}";
    }
}

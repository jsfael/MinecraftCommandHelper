using MinecraftCommandAPI.Models;

public class FillCommand
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }
    public string Block { get; set; }
    private string mode;
    public string Mode
    {
        get => mode;
        set
        {
            if (value != null && value != "destroy" && value != "keep" && value != "outline" && value != "hollow" && value != "replace")
            {
                throw new ArgumentException("Mode must be one of the following: destroy, keep, outline, hollow, replace.");
            }
            mode = value;
        }
    }
    public string OldBlock { get; set; } 

    public FillCommand(double width, double height, double depth, string block, string mode = null, string oldBlock = null)
    {
        Width = width;
        Height = height;
        Depth = depth;
        Block = block;
        Mode = mode;
        OldBlock = oldBlock;
    }

    public override string ToString()
    {
        string command = $"/fill ~ ~ ~ ~+{Width - 1} ~+{Height - 1} ~+{Depth - 1} minecraft:{Block}";

        if (!string.IsNullOrEmpty(Mode))
        {
            command += $" {Mode}";
        }

        if (Mode == "replace" && !string.IsNullOrEmpty(OldBlock))
        {
            command += $" minecraft:{OldBlock}";
        }

        return command;
    }
}

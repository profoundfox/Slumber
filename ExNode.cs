public class ExampleNode : Node
{
    public int Health { get; set; }
    public bool Quit { get; set; }
    public ExampleNode(NodeConfig config) : base(config)
    {
        
    }
}
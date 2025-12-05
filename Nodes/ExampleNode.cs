using Monlith.Nodes;

namespace Slumber;

public record class ExNodeConfig : SpatialNodeConfig
{
    public bool Bool { get; set; }
    
}

public class ExampleNode : Node2D
{
    public ExampleNode(ExNodeConfig cfg) : base(cfg)
    {
        
    }

    public override void Load()
    {
        base.Load();
    }

    public override void Unload()
    {
        base.Unload();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}



namespace Slumber;

public class FreeBox : Node2D
{   
    Area2D FreeArea;
    
    public FreeBox(SpatialNodeConfig config) : base(config) {}

    public override void Load()
    {
        base.Load();

        FreeArea = new(new AreaConfig
        {
            Region = Region,
            Name = "FreeBoxArea",
            Parent = this,
            MonitorAreas = true,
            MonitorBodies = true
        });
    }

    public override void Unload()
    {
        base.Unload();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (FreeArea.BodyEntered(out KinematicBody2D body) && body is Player player)
        {
            PlayerData.Dead = true;
            player.QueueFree();
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

}

namespace Slumber;

public class Checkpoint : Node2D
{

  public AnimatedSprite2D Explo { get; set; }
  public Sprite2D Sprite { get; set; }

  public Area2D Area { get; set; }

  //public string Id { get; set; } 

  public bool Lit { get; set; }

  public override void EnterTree()
  {
    var atlas = AsepriteLoader.LoadAnimation(
        new TextureRegion(Core.Resource.Load<Texture2D>("Graphics/Explosion"), new Rectangle(0, 0, 1148, 141)),
        new Extent(164, 141)
    );

    Sprite = new Sprite2D().Set(n =>
    {
      n.Origin = Vector2.Zero;
      n.Texture = new TextureRegion(Core.Resource.Load<Texture2D>("Maps/Atlas"), new Rectangle(512, 336, 64, 32));
      n.HFrames = 2;
      n.Frame = 0;
      n.SetParent(this);
    });

    Explo = new AnimatedSprite2D().Set(n =>
    {
      n.Atlas = atlas;
      n.Position = new Vector2(20, -30);
      n.SetParent(this);
    });

    if (Main.GameManager.JangoPersistence.CheckpointTriggered == this.Name)
    {
      Lit = true;
      Sprite.Frame = 1;
      Explo.Visible = false;
      Main.GameManager.Save(this);
    }

    Area = new Area2D().Set(n =>
    {
      n.AddChild(new CollisionShape2D().Set(c =>
      {
        c.Position = new Vector2(0, 0);
        c.Shape = new RectangleShape2D(30, 32);
      }));
      n.SetParent(this);
    });
  }

  public override void PhysicsUpdate(float delta)
  {

  }

  public override void Process(float delta)
  {
    if (Area.GetAnyBody() is Player p && !Lit)
    {
      Light();
    }
  }
  
  public void Light()
  {
    Core.Token.Get<PixelCamera>().Shake(TimeSpan.FromSeconds(0.1f));
    Lit = true;
    Main.GameManager.JangoPersistence.CheckpointTriggered = this.Name;
    Sprite.Frame = 1;
    Explo.PlayAnimation("Main", false);
    Await.Until(() => Explo.IsFinished, () => Explo.Visible = false);
    Main.GameManager.Save(this);
  }

  public override void ExitTree()
  {

  }
}

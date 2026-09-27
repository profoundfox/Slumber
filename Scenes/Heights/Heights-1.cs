using System.IO;
using DotTiled.Serialization;

namespace Slumber;

public class Heights1 : Scene
{
  public Player Player;

  public override void EnterTree()
  {
    Player = new Player();

    var loader = Loader.Default();
    var mapPath = Path.Combine(
        AppContext.BaseDirectory,
        "Content",
        "Maps",
        "Heights",
        "heights-1.tmx"
    );

    var root = new Node2D()
      .Set("Position", new Vector2(0, -250))
      .Set(n => n.Visible = true);

    new Parallax2D().Set(n =>
    {
      n.Texture = new TextureRegion(Core.Resource.Load<Texture2D>("Graphics/Background/Heights/Tree1"), new Rectangle(0, 0, 640, 360));
      n.Depth = -8;
      n.MotionScale = new Vector2(0.2f, 0f);
      n.RepeatSize = new Extent(640, 0);
      n.RepeatTimes = 4;
      n.SetParent(root);
    });

    new Parallax2D().Set(n =>
    {
      n.Texture = new TextureRegion(Core.Resource.Load<Texture2D>("Graphics/Background/Heights/Tree2"), new Rectangle(0, 0, 640, 360));
      n.Depth = -9;
      n.MotionScale = new Vector2(0.3f, 0f);
      n.RepeatTimes = 4;
      n.RepeatSize = new Extent(640, 0);
      n.SetParent(root);
    });

    new Parallax2D().Set(n =>
    {
      n.Texture = new TextureRegion(Core.Resource.Load<Texture2D>("Graphics/Background/Heights/Tree3"), new Rectangle(0, 0, 640, 360));
      n.Depth = -10;
      n.MotionScale = new Vector2(0.4f, 0f);
      n.RepeatTimes = 4;
      n.RepeatSize = new Extent(640, 0);
      n.SetParent(root);
    });

    new Parallax2D().Set(n =>
    {
      n.Texture = new TextureRegion(Core.Resource.Load<Texture2D>("Graphics/Background/Heights/Tree4"), new Rectangle(0, 0, 640, 360));
      n.Depth = -11;
      n.MotionScale = new Vector2(0.5f, 0f);
      n.RepeatSize = new Extent(640, 0);
      n.RepeatTimes = 4;
      n.SetParent(root);
    });

    new Parallax2D().Set(n =>
    {
      n.Texture = new TextureRegion(Core.Resource.Load<Texture2D>("Graphics/Background/Heights/Mountain"), new Rectangle(0, 0, 640, 360));
      n.Depth = -12;
      n.MotionScale = new Vector2(0.6f, 0f);
      n.RepeatTimes = 4;
      n.RepeatSize = new Extent(640, 0);
      n.SetParent(root);
    });

    new Parallax2D().Set(n =>
    {
      n.Texture = new TextureRegion(Core.Resource.Load<Texture2D>("Graphics/Background/Heights/Gradient"), new Rectangle(0, 0, 640, 360));
      n.Depth = -13;
      n.MotionScale = new Vector2(0.7f, 0f);
      n.RepeatTimes = 4;
      n.RepeatSize = new Extent(640, 0);
      n.SetParent(root);
    });

    new CanvasAnchor().Set(n =>
    {
      n.BackBufferColor = new Color(8, 10, 12);
      n.AmbientColor = Color.White;
    });
    
    var t = DotTiledBridge.Load(mapPath, this, loader);

    new PixelCamera()
      .Set(n => n.Weight = 0.3f)
      .Set(n => n.Limit = CameraRect)
      .Set(n => n.Deadzone = new Extent(30, 0))
      .Set(n => n.OffsetSmoothing = true)
      .Set(n => n.Smoothing = true)
      .Set(n => n.Target = Player);
  }

  public override void Process(float delta)
  {

  }

  public override void PhysicsUpdate(float delta)
  {

  }

  public override void ExitTree()
  {

  }
}





using System.IO;
using System.Linq;
using DotTiled.Serialization;
using MonoTile;

namespace Slumber;

public class Caverns1 : Scene
{
  public Player Player;

  public override void EnterTree()
  {
    base.EnterTree();

    var root = new Node2D()
      .Set("Position", new Vector2(0, -100));
    
    var playerPos = new Vector2(104, -24);
    var playerDir = -1;

    Player = new Player();

    var rect = new Rectangle(-280, -232, 640, 360);

    var loader = Loader.Default();
    var mapPath = Path.Combine(
        AppContext.BaseDirectory,
        "Content",
        "Maps",
        "Caverns",
        "caverns-1.tmx"
    );
    
    new CanvasAnchor().Set(n =>
    {
      n.BackBufferColor = new Color(49, 49, 56);
      //n.AmbientColor = Color.Gray;
    });
    
    var t = DotTiledBridge.Load(mapPath, this, loader);

    new PixelCamera()
      .Set(n => n.Weight = 0.3f)
      .Set(n => n.Limit = CameraRect)
      .Set(n => n.Deadzone = new Extent(30, 30))
      .Set(n => n.OffsetSmoothing = true)
      .Set(n => n.Smoothing = true)
      .Set(n => n.Target = Player);


    string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    string myGameFolder = System.IO.Path.Combine(saveFolder, "Slumber");
    System.IO.Directory.CreateDirectory(myGameFolder);

    if (!File.Exists(Path.Combine(myGameFolder, "Persistence")))
      Main.GameManager.Save("Caverns1", SpawnPoints.FirstOrDefault().Value.Position);
  }

  public override void ExitTree()
  {
    base.ExitTree();
  }

  public override void PhysicsUpdate(float delta)
  {
    base.PhysicsUpdate(delta);
  }

  Vector2 offset = new Vector2(20, 0);

  public override void Process(float delta)
  {
    base.Process(delta);

    if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Y))
      new Enemy().Set("Position", Core.Token.Get<Player>()?.Transform.Global.Position + offset);
  }

  public override void Submit(Canvas2D canvas)
  {
    base.Submit(canvas);
  }
}

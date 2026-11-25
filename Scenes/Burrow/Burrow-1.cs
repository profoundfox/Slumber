using System.IO;
using DotTiled.Serialization;

namespace Slumber
{
  public class Burrow1 : Scene
  {
    Player Player;

    public override void EnterTree()
    {
      Player = new Player();

      var loader = Loader.Default();
      var mapPath = Path.Combine(
          AppContext.BaseDirectory,
          "Content",
          "Maps",
          "Burrow",
          "burrow-1.tmx"
      );

      var root = new Node2D()
        .Set("Position", new Vector2(0, -250))
        .Set(n => n.Visible = true);

      new CanvasAnchor().Set(n =>
      {
        n.BackBufferColor = new Color(154, 39, 12);
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
  }
}

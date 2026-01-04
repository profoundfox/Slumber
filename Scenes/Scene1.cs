

using System.Linq;

namespace Slumber;

public class Scene1 : Scene, IStage
{
    public Camera2D Camera { get; set; }

    float val = 0;
    
    public Scene1 ():  base(new SceneConfig
    {
        DataPath = "Raw/LevelData/Scene1.json",
        TilemapTexturePath = "Assets/Tileset/SlumberTilesetAtlas",
        TilemapRegion = new Rectangle(0, 0, 512, 512)
    }) {  }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Load()
    {
        base.Load();
        
        Camera = new RoomCamera(new RoomCameraConfig
        {
            TargetNode = Engine.Node.GetFirstNodeByT<Player>()
        });

        Camera.LocalPosition = Engine.Node.GetFirstNodeByT<Player>().GlobalPosition;
        
        var par = new Parallax2D(new ParallaxConfig
        {
            LocalPosition = new Vector2(0, -50)
        });

        var layer2 = new ParallaxLayer(new ParallaxLayerConfig
        {
            Parent = par,
            Texture = new MTexture("Assets/Backgrounds/HeightsBG"),
            MotionScale = Vector2.Zero,
            LoopAxes = LoopAxis.X
        });

        var layer1 = new ParallaxLayer(new ParallaxLayerConfig
        {
            Parent = par,
            Texture = new MTexture("Assets/Backgrounds/HeightsBGNoMain"),
            MotionScale = Vector2.Zero
        });
    }

    public override void Unload()
    {
        base.Unload();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Console.WriteLine(val);
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}

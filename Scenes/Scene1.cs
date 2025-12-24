
using RenderingLibrary.Graphics;

namespace Slumber;

public class Scene1 : Scene, IScene
{
    public RoomCamera Camera { get; set; }

    public Effect Blur;

    Sprite2D sprite;
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
        
        GumHelper.Wipe();

        Camera = new RoomCamera(new RoomCameraConfig
        {
            LocalPosition = NodeManager.GetFirstNodeByT<Player>().LocalPosition,
            TargetNode = NodeManager.GetFirstNodeByT<Player>()
        });

        new ParallaxLayer(new ParallaxLayerConfig
        {
            Texture = new MTexture("Assets/Backgrounds/streetsbg"),
            LoopTimes = 1
        });
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

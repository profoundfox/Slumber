

namespace Slumber;

public class Scene1 : Scene, IScene
{
    public Camera2D Camera { get; set; }
    
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
        
        Camera = new Camera2D(new CameraConfig
        {
            Parent = Engine.Node.GetFirstNodeByT<Player>()
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

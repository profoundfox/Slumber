namespace Slumber;

public class Scene1 : Scene, IScene
{
    public RoomCamera _camera { get; set; }
    public ParallaxBackground Background;

    public Scene1() {  }
    public void Initialize()
    {
        GumHelper.RemoveScreenOfType<TitleScreen>();
    }
    public void Load()
    {
        OgmoParser.FromFile("Data/Scene1.json", "Assets/Tileset/SlumberTilesetAtlas", "0 0 512 512");

        _camera = new RoomCamera(1f); 

        
    }

    public void Unload() {  }

    public void Update(GameTime gameTime)
    {
  
        if (Engine.Input.Keyboard.WasKeyJustPressed(Keys.R))
        {
            Engine.SceneManager.ReloadCurrentScene();
        }

        ConstructObject.UpdateObjects(gameTime);

        _camera.Follow(KinematicEntity.EntityList.OfType<Player>().FirstOrDefault());
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var s in Engine.SpriteManager.Sprites) 
        {
            Engine.DrawManager.Draw(s);
        }

        ConstructObject.DrawObjects(spriteBatch);

        Tilemap.DrawTilemaps(spriteBatch);

        Engine.DrawManager.Flush();
    }

}

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

        Engine.DrawManager.SetCamera(_camera.Transform);
        
        Background = new ParallaxBackground(
            texture: Engine.Content.Load<Texture2D>("Assets/Backgrounds/Desert"),
            parallaxFactor: 0.2f,
            samplerState: ParallaxSamplers.RepeatX,
            camera: _camera
        );

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
        Background.Draw(spriteBatch, Engine.GraphicsDevice);

        Engine.SpriteManager.DrawAllSprites(Engine.SpriteBatch);

        spriteBatch.Begin(
            SpriteSortMode.BackToFront,
            samplerState: SamplerState.PointClamp,
            transformMatrix: _camera.Transform
        );

        ConstructObject.DrawObjects(spriteBatch);

    
        Tilemap.DrawTilemaps(spriteBatch);
                
        spriteBatch.End();
    }

}

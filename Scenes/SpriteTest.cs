namespace Slumber;

public class SpriteTest : Scene, IScene
{   
    private TextureAtlas _atlas;
    public AnimatedSprite AnimatedSprite;
    
    public SpriteTest() {  }
    public void Initialize()
    {
        
    }
    public void Load()
    {
        _atlas = TextureAtlas.FromFile(Engine.Content, "Assets/Atlas/Player/player-atlas.xml", "Assets/Animations/Player/PlayerModel3Atlas");

        OgmoParser.FromFile("Data/Scene1.json", "Assets/Tileset/SlumberTilesetAtlas", "0 0 512 512");

        AnimatedSprite = _atlas.CreateAnimatedSprite("idle-animation");
        AnimatedSprite.LayerDepth = 0.5f;

        AnimatedSprite.Position = new Vector2(100, 100);

    }

    public void Unload() {  }

    public void Update(GameTime gameTime)
    {
        AnimatedSprite.Update(gameTime);
    }



    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var sprite in Engine.SpriteManager.Sprites)
            Engine.DrawManager.Draw(sprite);

        Engine.DrawManager.Flush();
    }

}

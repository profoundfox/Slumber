namespace Slumber;

public class SpriteTest : Scene, IScene
{   
    private TextureAtlas _atlas;
    public AnimatedSprite AnimatedSprite;
    
    public SpriteTest() : base(new SceneConfig()) {}

    public override void Initialize()
    {
        
    }
    public override void Load()
    {
        _atlas = TextureAtlas.FromFile(Engine.Content, "Assets/Atlas/Player/player-atlas.xml", "Assets/Animations/Player/PlayerModel3Atlas");
        AnimatedSprite = _atlas.CreateAnimatedSprite("idle-animation");
        AnimatedSprite.LayerDepth = 0.5f;

        AnimatedSprite.Position = new Vector2(100, 100);

    }

    public override void Unload() {  }

    public override void Update(GameTime gameTime)
    {
        AnimatedSprite.Update(gameTime);
    }



    public override void Draw(SpriteBatch spriteBatch)
    {
        


        Engine.DrawManager.Flush();
    }

}

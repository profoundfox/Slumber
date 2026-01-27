namespace Slumber;

public class Scene1 : Scene, IStage
{
    public Camera2D Camera { get; set; }    
    public Scene1 ():  base(new SceneConfig
    {
        DataPath = "Raw/LevelData/Level1.json",
        TilemapTexturePath = "Assets/Tileset/SlumberTilesetAtlas",
        TilemapRegion = new Rectangle(0, 0, 512, 512)
    }) {  }

    public override void OnEnter()
    {
        base.OnEnter();
        
        Camera = new RoomCamera(new RoomCameraConfig
        {
            TargetNode = Engine.Node.GetFirstNodeByT<Player>()
        });

        Camera.LocalPosition = new Vector2(Engine.Node.GetFirstNodeByT<Player>().GlobalPosition.X, Engine.Node.GetFirstNodeByT<Player>().GlobalPosition.Y - 60);

        var layer1 = new ParallaxLayer(new ParallaxLayerConfig
        {
            Texture = new MTexture("Assets/Backgrounds/HeightsBGNoMain"),
            LoopAxis = LoopAxis.X,
            LocalPosition = new Vector2(0, 250)
        });

        var layer2 = new ParallaxLayer(new ParallaxLayerConfig
        {
            Texture = new MTexture("Assets/Backgrounds/HeightsBG"),
            MotionScale = Vector2.Zero,
            LoopAxis = LoopAxis.X,
            LocalPosition = new Vector2(0, 300)
        });
    }

    public override void OnExit()
    {
        base.OnExit();
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

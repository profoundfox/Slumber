namespace Slumber;

public class Scene1 : Scene, IScene
{
    public RoomCamera Camera { get; set; }

    public Scene1 ():  base(new SceneConfig
    {
        DataPath = "Data/Scene1.json",
        TilemapTexturePath = "Assets/Tileset/SlumberTilesetAtlas",
        TilemapRegion = "0 0 512 512"
    }) {  }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Load()
    {
        base.Load();
        
        GumHelper.Wipe();
        Camera = new RoomCamera(1f);
        Camera.LerpFactor = 1f;

        var sprite = new Sprite2D(new NodeConfig
        {
            Parent = this,
            Name = "SpriteOne",
            Shape = new RectangleShape2D(10, 10, 10, 10)
        });

        sprite.Texture = new MTexture("Assets/Animations/Enemies/grassspidersheet");

        var exNode = new ExampleNode(new NodeConfig
        {
            Parent = this,
            Name = "ExNode",
            Position = new Vector2(40, 10)
        });

        var exNode2 = new ExampleNode(new NodeConfig
        {
            Parent = this,
            Name = "ExNode"
        });
    }

    public override void Unload()
    {
        base.Unload();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        foreach (var node in NodeManager.AllInstances)
            Console.WriteLine(node.Position);
        Camera.Follow(NodeManager.AllInstances.OfType<Player>().FirstOrDefault());

    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);        
    }
}

using System.Collections.Generic;
using Monlith.Nodes;

namespace Slumber;

public class Scene1 : Scene, IScene
{
    public RoomCamera Camera { get; set; }

    public Player Player;

    Area2D Area1;
    Area2D Area2;

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

        Player = new Player(new KinematicBaseConfig
        {
            Parent = null,
            Name = "Player",
            Position = new Vector2(100, 100),
            CollisionShape2D = new CollisionShape2D(new CollisionShapeConfig
            {
                Position = new Vector2(100, 100),
                Shape = new RectangleShape2D(10, 25)
            })
        });

        NodeFactory.CreateNode("Area2D", new RectangleShape2D(100, 100, 100, 100));
    }

    public override void Unload()
    {
        base.Unload();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Camera.Follow(NodeManager.GetNodeByType<Player>());
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);       
    }
}

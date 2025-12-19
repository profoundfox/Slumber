using System.Collections.Generic;
using Microsoft.VisualBasic;
using Monlith.Nodes;
using RenderingLibrary;

namespace Slumber;

public class Scene1 : Scene, IScene
{
    public RoomCamera Camera { get; set; }

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
        //Camera = new Monlith.Util.RoomCamera(1f, NodeManager.GetNodeByType<Player>().Position);
        //Camera.LerpFactor = 1f;

        var camera = new RoomCamera(new RoomCameraConfig
        {
            Position = NodeManager.GetNodeByType<Player>().Position,
            TargetNode = NodeManager.GetNodeByType<Player>()
        });
        
    }

    public override void Unload()
    {
        base.Unload();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        //Camera.Follow(NodeManager.GetNodeByType<Player>());
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);       
    }
}

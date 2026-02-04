using System.IO;
using System.Security.Cryptography;
using RenderingLibrary.Graphics;

namespace Slumber;

public class Scene1 : IStage
{
    public Scene1 () {}

    public Song bgMusic;
    public SoundEffect bgSFX;

    Random random;

    public void OnEnter()
    {       
        
        random = new Random();

        bgMusic = Engine.Resource.Load<Song>("Assets/Music/Cold_Heights_Ambient_01");
        bgSFX = Engine.Resource.Load<SoundEffect>("Assets/Music/blizzard-445020");

        MediaPlayer.IsRepeating = true;

        //MediaPlayer.Play(bgMusic);
        
        var bgSFXInstance = bgSFX.CreateInstance();

        bgSFXInstance.IsLooped = true;

        //bgSFXInstance.Play();

        var camera = new RoomCamera(new RoomCameraConfig
        {
            TargetNode = Engine.Node.GetFirstNodeByT<Player>()
        });

        camera.LocalPosition = new Vector2(Engine.Node.GetFirstNodeByT<Player>().GlobalPosition.X, Engine.Node.GetFirstNodeByT<Player>().GlobalPosition.Y - 60);

        var layer1 = new ParallaxLayer(new ParallaxLayerConfig
        {
            Texture = new MTexture("Assets/Backgrounds/HeightsBGNoMain"),
            LoopAxis = LoopAxis.X,
            LocalPosition = new Vector2(0, 250),
            Depth = -2
        });

        var layer2 = new ParallaxLayer(new ParallaxLayerConfig
        {
            Texture = new MTexture("Assets/Backgrounds/HeightsBG"),
            MotionScale = Vector2.Zero,
            LoopAxis = LoopAxis.X,
            LocalPosition = new Vector2(0, 300),
            Depth = -1
        });
    }

    public void OnExit() {}

    public void Update(GameTime gameTime)
    {
        
    }
    
    public void SubmitCall()
    {
        foreach (var c in Engine.Node.GetNodesByT<RayCast2D>()) 
            c.Ray.Draw();        
    }
}

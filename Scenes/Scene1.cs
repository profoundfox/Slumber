namespace Slumber;

public class Scene1 : Scene, IScene
{
    public FollowCamera Camera { get; set; }

    CollisionObject collisionObject;
    float colPosXTarget = -100f;

    Tween tween;

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

        collisionObject = new CollisionObject(new Rectangle((int)colPosXTarget, 150, 100, 20), true, false);

        tween = new Tween(
            100f,
            EasingFunctions.Linear,
            t => colPosXTarget = MathHelper.Lerp(colPosXTarget, 500f, t),
            () => tween.Sta
        );
        
        tween.Start();
        Engine.TweenManager.AddTween(tween);

        Camera = new FollowCamera(1f); 
        Camera.LerpFactor = 1f;
    }

    public override void Unload()
    {
        base.Unload();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        Camera.Follow(KinematicEntity.EntityList.OfType<Player>().FirstOrDefault().KinematicBase.Collider.Rect);

        if (Engine.Input.Keyboard.WasKeyJustPressed(Keys.R))
        {
            Engine.SceneManager.ReloadCurrentScene();
        }

        collisionObject.Position = new Vector2(colPosXTarget, 150);


    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        DrawHelper.DrawRectangle(collisionObject.Area.Rect, Color.Red, 2);
    }

}

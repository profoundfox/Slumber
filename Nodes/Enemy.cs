
using System.Collections.Generic;

namespace Slumber.Nodes;

public class Enemy : KinematicBody2D
{
    public float Gravity = 1300f;
    private int Health;
    private int Direction = 1;

    private RayCast2D EnemyRay;
    private RayCast2D EnemyRayNotDown;

    private Vector2 SpritePosition;
    Vector2 RayPos;


    private bool CanTakeDamage = true;

    Animation RunAnimation;
    AnimatedSprite AnimatedSprite;
    public Enemy(KinematicBaseConfig config) : base(config) {}

    public override void Load()
    {
        MTexture EnemyTexture = new("Assets/Animations/Enemies/grassspidersheet");

        var animations = AsepriteLoader.LoadAnimations(EnemyTexture, "Raw/EnemyModel.json");

        RunAnimation = animations["Run"];

        AnimatedSprite = new(RunAnimation);
        AnimatedSprite.LayerDepth = 0.5f;


        Health = 5;

        float forwardOffset = CollisionShape2D.Shape.BoundingBox.Width / 2f + 5f;

        RayPos = new Vector2
        (
            CollisionShape2D.Shape.BoundingBox.Center.X + forwardOffset * Direction,
            CollisionShape2D.Shape.BoundingBox.Bottom
        );

        EnemyRay = new RayCast2D(RayPos, 90, 50);
        EnemyRayNotDown = new RayCast2D(RayPos, 0, 5);
    }

    public override void Update(GameTime gameTime)
    {
        float forwardOffset = CollisionShape2D.Shape.BoundingBox.Width / 2f + 5f;

        RayPos.X = CollisionShape2D.Shape.BoundingBox.Center.X + forwardOffset * Direction;
        RayPos.Y = CollisionShape2D.Shape.BoundingBox.Bottom;

        EnemyRay.Update(RayPos, 90, 50);
        EnemyRayNotDown.Update(RayPos, 0, 5);

        //EnemyRay.CheckIntersection(NodeManager.AllInstances.OfType<StaticBody2D>());
        //EnemyRayNotDown.CheckIntersection(NodeManager.AllInstances.OfType<StaticBody2D>());

        HandleGravity();

        HandleMovement();

        FlipSprite(Direction);

        UpdateKinematicBody();

        if (IsOnGround())
        {
            Velocity.Y = 0;
        }

        SpritePosition = Position;

        AnimatedSprite.PlayAnimation(RunAnimation, true);

        AnimatedSprite.Update(gameTime);

        AnimatedSprite.Position = new Vector2(SpritePosition.X , SpritePosition.Y);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        Engine.DrawManager.Draw(AnimatedSprite);
    }

    private void FlipSprite(int direction)
    {
        if (direction == 1)
            if (AnimatedSprite != null) AnimatedSprite.Effects = SpriteEffects.None;
        else
            if (AnimatedSprite != null) AnimatedSprite.Effects = SpriteEffects.FlipHorizontally;
    }

    private void HandleGravity()
    {
        Velocity.Y += Gravity * Engine.DeltaTime;
    }

    private void HandleMovement()
    {

        if (!EnemyRay.HasHit || EnemyRayNotDown.HasHit)
        {
            Direction = -Direction;
        }

        Velocity.X = 80 * Direction;
    }

    public void TakeDamage(int DamageAmount)
    {
        if (!CanTakeDamage) return;
        CanTakeDamage = false;
        Health -= DamageAmount;

        CTimer.Wait(0.7f, () => { CanTakeDamage = true; });
    }

}

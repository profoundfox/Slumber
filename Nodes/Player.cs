using System.Collections.Generic;
using Monolith.Nodes;
using RenderingLibrary;

namespace Slumber;

public class Player : KinematicBody2D
{
    public int AttackColliderOffset;

    public PlayerInfo PlayerInfo = new();
    public bool Dead;

    public AnimatedSprite2D AnimatedSprite;
    public StateController StateController;
    
    public Area2D AttackZone;

    public int PlayerDirection = 1;
    public int PlayerAxis = 1;

    public Player(KinematicBaseConfig config) : base(config) {}

    public override void Load()
    {
        MTexture PlayerTexture = new("Assets/Animations/PlayerModel3Atlas");

        var animations = AsepriteLoader.LoadAnimations(PlayerTexture, "Raw/Raw/PlayerModel3.json");
        
        AnimatedSprite = new AnimatedSprite2D(new AnimatedSpriteConfig
        {
            Parent = this,
            Name = "PlayerSprite",
            Atlas = animations,
            LocalPosition = new Vector2(2, 10),
            IsLooping = true
        });

        CollisionShape2D = new CollisionShape2D(new CollisionShapeConfig
        {
            Parent = this,
            Shape = new RectangleShape2D(10, 25)
        });

        LocalOrdering = LocalOrdering with { Depth = 3 };
        
        var idle = new PlayerIdleState(this);
        var run = new PlayerRunState(this);
        var attack = new PlayerAttackState(this);
        var jump = new PlayerJumpState(this);
        var fall = new PlayerFallState(this);
        var wallSlide = new PlayerWallSlideState(this);
        var wallJump = new PlayerWallJumpState(this);
        

        StateController = new StateController(idle,
        [
            idle, run, attack, jump, fall, wallSlide, wallJump
        ]);

    }

    public override void Update(GameTime gameTime)
    {   
        base.Update(gameTime);

        PlayerAxis = Engine.Input.GetAxis("MoveLeft", "MoveRight");

        StateController.Update(gameTime);

        SaveManager.PlayerData.CurrentPosition = LocalPosition;

        FlipSprite();

        if (PlayerInfo.AttackCount == 2)
            PlayerInfo.AttackCount = 0;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    public void ApplyGravity()
    {
        if (!IsOnGround())
        {
            Velocity.Y = MathF.Min(
                Velocity.Y + PlayerInfo.Gravity * Engine.DeltaTime,
                PlayerInfo.TerminalVelocity
            );
        }
        else if (Velocity.Y > 0)
        {
            Velocity.Y = 0;
        }
    }


    public void HandleMovementInput()
    {
        PlayerDirection = PlayerAxis != 0 ? PlayerAxis : PlayerDirection;
        
        float targetSpeed = PlayerInfo.MoveSpeed * PlayerAxis;

        if (Engine.Input.IsActionPressed("MoveUp") && !Engine.Input.IsActionPressed("MoveDown"))
        {
            Velocity.Y = -130;
        }

         if (Engine.Input.IsActionPressed("MoveDown") && !Engine.Input.IsActionPressed("MoveUp"))
        {
            Velocity.Y = 130;
        }

        if (targetSpeed != 0)
        {
            Velocity.X = MoveToward(Velocity.X, targetSpeed, PlayerInfo.Acceleration * Engine.DeltaTime);
        }
    }

    public void HandleDeceleration()
    {
        if (!Engine.Input.IsActionPressed("MoveLeft") && !Engine.Input.IsActionPressed("MoveRight"))
        {
            Velocity.X = MoveToward(Velocity.X, 0, PlayerInfo.Deceleration * Engine.DeltaTime);
        }
    }

    public void FlipSprite()
    {
        if (PlayerAxis == 1)
        {
            AttackColliderOffset = 15;
            AnimatedSprite.LocalMaterial = AnimatedSprite.LocalMaterial with { SpriteEffects = SpriteEffects.None };
        }
        if (PlayerAxis == -1)
        {
            AttackColliderOffset = -38;
            AnimatedSprite.LocalMaterial = AnimatedSprite.LocalMaterial with { SpriteEffects = SpriteEffects.FlipHorizontally };
        }
    }
}

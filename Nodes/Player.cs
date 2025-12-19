using System.Collections.Generic;
using Monlith.Nodes;
using RenderingLibrary;

namespace Slumber;

public class Player : KinematicBody2D
{
    public int AttackColliderOffset;

    public PlayerInfo PlayerInfo = new();
    public bool Dead;

    public AnimatedSprite2D AnimatedSprite;
    public StateController StateController;

    private PlayerUI Screen;
    private Pausemenu pauseMenu;

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
            Position = new Vector2(5, 10),
            IsLooping = true
        });

        CollisionShape2D = new CollisionShape2D(new CollisionShapeConfig
        {
            Parent = this,
            Shape = new RectangleShape2D(10, 25),
        });

        AnimatedSprite.LayerDepth = 0.5f;
        
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

        pauseMenu = new Pausemenu();
    }

    public override void Update(GameTime gameTime)
    {   
        UpdateKinematicBody();

        PlayerAxis = Engine.Input.GetAxis("MoveLeft", "MoveRight");

        StateController.Update(gameTime);

        SaveManager.PlayerData.CurrentPosition = Position;

        Console.WriteLine($"Position: {Position} Velocity: {Velocity}");

        FlipSprite();

        if (PlayerInfo.AttackCount == 2)
            PlayerInfo.AttackCount = 0;        
    }

    public override void Draw(SpriteBatch spriteBatch) { }

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
        float targetSpeed = 0f;

        if (Engine.Input.IsActionPressed("MoveLeft") && !Engine.Input.IsActionPressed("MoveRight"))
        {
            targetSpeed = -PlayerInfo.MoveSpeed;
            PlayerDirection = -1;
        }
        else if (Engine.Input.IsActionPressed("MoveRight") && !Engine.Input.IsActionPressed("MoveLeft"))
        {
            targetSpeed = PlayerInfo.MoveSpeed;
            PlayerDirection = 1;
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
            AnimatedSprite.Effects = SpriteEffects.None;


        }
        if (PlayerAxis == -1)
        {
            AttackColliderOffset = -38;
            AnimatedSprite.Effects = SpriteEffects.FlipHorizontally;
        }
    }
}

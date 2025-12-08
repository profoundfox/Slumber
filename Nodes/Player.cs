using Monlith.Nodes;

namespace Slumber.Nodes;

public class Player : KinematicBody2D
{
    public int AttackColliderOffset;

    public PlayerInfo PlayerInfo = new();
    public bool Dead;

    private PlayerUI Screen;
    private Pausemenu pauseMenu;
    public AnimatedSprite2D AnimatedSprite;
    public StateController StateController;

    public Area2D AttackZone;

    public int PlayerAxis;

    public Player(KinematicBaseConfig config) : base(config) {}

    public override void Load()
    {
        Screen = new PlayerUI();
        pauseMenu = new Pausemenu();

        MTexture PlayerTexture = new("Assets/Animations/Player/PlayerModel3Atlas");

        AnimatedSprite = new AnimatedSprite2D(new AnimatedSpriteConfig
        {
            Parent = this,
            Name = "PlayerSprite",
            Atlas = AsepriteLoader.LoadAnimations(PlayerTexture, "Raw/PlayerModel3.json"),
            Position = new Vector2(Position.X, Position.Y + 10),
            IsLooping = true
        });

        AttackZone = new Area2D(new AreaConfig
        {
            Parent = this,
            Name = "PlayerAttackZone",
            Position = Position,
            CollisionShape2D = new CollisionShape2D(new CollisionShapeConfig
            {
                Shape = new CircleShape2D(15),
                Position = Position
            })
        });

        CollisionShape2D.Width = 10;
        CollisionShape2D.Height = 25;

        AnimatedSprite.LayerDepth = 0.5f;
        
        var grounded = new PlayerGroundedState(this);
        var idle = new PlayerIdleState(this);
        var run = new PlayerRunState(this);
        var attack = new PlayerAttackState(this);
        var jump = new PlayerJumpState(this);
        var fall = new PlayerFallState(this);
        var wallSlide = new PlayerWallSlideState(this);
        var wallJump = new PlayerWallJumpState(this);

        idle.SetParent(grounded);
        run.SetParent(grounded);
        attack.SetParent(grounded);
        

        StateController = new StateController(idle,
        [
            grounded, idle, run, attack, jump, fall, wallSlide, wallJump
        ]);

        CollisionShape2D.Disabled = false;
    }

    public override void Update(GameTime gameTime)
    {   
        UpdateKinematicBody();

        PlayerAxis = Engine.Input.GetAxis("MoveLeft", "MoveRight");

        ApplyGravity();

        StateController.Update(gameTime);

        SaveManager.PlayerData.CurrentPosition = Position;

        FlipSprite();


        AnimatedSprite.Position = new Vector2(Position.X + PlayerInfo.textureOffset, Position.Y + 10);
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

    public void HandleHorizontalInput()
    {
        float targetSpeed = 0f;

        if (Engine.Input.IsActionPressed("MoveLeft") && !Engine.Input.IsActionPressed("MoveRight"))
        {
            targetSpeed = -PlayerInfo.MoveSpeed;
            PlayerInfo.dir = -1;
        }
        else if (Engine.Input.IsActionPressed("MoveRight") && !Engine.Input.IsActionPressed("MoveLeft"))
        {
            targetSpeed = PlayerInfo.MoveSpeed;
            PlayerInfo.dir = 1;
        }

        float accel = (MathF.Abs(targetSpeed) > 0) ? PlayerInfo.Acceleration : PlayerInfo.Deceleration;
        Velocity.X = MoveToward(Velocity.X, targetSpeed, accel * Engine.DeltaTime);
    }

    public void FlipSprite()
    {
        if (PlayerInfo.dir == 1)
        {
            AttackColliderOffset = 15;
            PlayerInfo.textureOffset = 0;
            AnimatedSprite.Effects = SpriteEffects.None;
        }
        else
        {
            AttackColliderOffset = -38;
            PlayerInfo.textureOffset = 8;
            AnimatedSprite.Effects = SpriteEffects.FlipHorizontally;
        }
    }
}

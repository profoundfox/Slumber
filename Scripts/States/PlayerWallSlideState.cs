
namespace Slumber;

public class PlayerWallSlideState : State
{
    protected Player p;
    public PlayerWallSlideState(Player player) => p = player;

    public override void OnEnter()
    {
        p.Velocity.Y = 0;
    }

    public void ApplyWallSlide()
    {
        p.Velocity.Y = MathF.Min(
            p.Velocity.Y + p.PlayerInfo.WallSlideGravity * Engine.DeltaTime,
            p.PlayerInfo.WallSlideGravity
        );
    }


    public override void Update(GameTime gameTime)
    {
        p.AnimatedSprite.PlayAnimation("Wall", false);

        ApplyWallSlide();
        
        if (!p.IsOnWall() || p.IsOnGround())
        {
            RequestTransition(nameof(PlayerFallState));
            return;
        }

        if (_checkWallExit())
        {
            RequestTransition(nameof(PlayerFallState));
            return;
        }

        if (Engine.Input.IsActionJustPressed("Jump"))
        {
            RequestTransition(nameof(PlayerWallJumpState));
            return;
        }

    }

    private bool _checkWallExit()
    {
        if (p.IsOnWall(out int wallDir))
        {
            return p.PlayerAxis == -wallDir;
        }

        return false;
    }

    public override void OnExit()
    {
    }
}

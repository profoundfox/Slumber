
namespace Slumber;

public class PlayerWallJumpState : State
{
    private readonly Player p;
    private float controlLockTimer = 0.06f;
    private bool controlRestored;

    public PlayerWallJumpState(Player player) => p = player;

    public override void OnEnter()
    {
        controlRestored = false;

        if (p.PlayerDirection == 1)
            p.Velocity.X = -p.PlayerInfo.WallJumpHorizontalSpeed;
        
         if (p.PlayerDirection == -1)
            p.Velocity.X = p.PlayerInfo.WallJumpHorizontalSpeed;

        p.Velocity.Y = -p.PlayerInfo.WallJumpVerticalSpeed;


    }

    public override void Update(float delta)
    {
        p.ApplyGravity();

        p.FlipSprite();


        if (!controlRestored)
        {
            Engine.Timer.Wait(controlLockTimer, () => { controlRestored = true;});
        }

        if (p.IsOnRoof())
        {
            p.Velocity.Y = 0;
        }

        if (controlRestored)
        {
            p.Velocity.X = 0;
            RequestTransition(nameof(PlayerFallState));
        }

        if (p.Velocity.Y > 0)
            RequestTransition(nameof(PlayerFallState));

        if (p.IsOnGround())
            RequestTransition(nameof(PlayerIdleState));
    }

    public override void OnExit()
    {
        controlRestored = true;
    }
}

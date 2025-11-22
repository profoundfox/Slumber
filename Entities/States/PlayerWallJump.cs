
namespace Slumber.Entities;

public class PlayerWallJumpState : State
{
    private readonly Player p;
    private float controlLockTimer = 0.06f;
    private bool controlRestored;

    public PlayerWallJumpState(Player player) => p = player;

    public override void OnEnter()
    {

        p.PlayerInfo.canMove = false;
        controlRestored = false;

        if (p.PlayerInfo.dir == 1)
            p.Velocity.X = -p.PlayerInfo.WallJumpHorizontalSpeed;
        
        else
            p.Velocity.X = p.PlayerInfo.WallJumpHorizontalSpeed;

        p.Velocity.Y = -p.PlayerInfo.WallJumpVerticalSpeed;


    }

    public override void Update(GameTime gameTime)
    {
        
        if (!controlRestored)
        {
            CTimer.Wait(controlLockTimer, () => { controlRestored = true; p.PlayerInfo.canMove = true; });
        }

        if (controlRestored)
            p.HandleHorizontalInput();

        if (p.Velocity.Y > 0)
            RequestTransition(nameof(PlayerFallState));

        if (p.IsOnGround())
            RequestTransition(nameof(PlayerIdleState));
    }

    public override void OnExit()
    {
        p.PlayerInfo.canMove = true;
        controlRestored = true;
    }
}

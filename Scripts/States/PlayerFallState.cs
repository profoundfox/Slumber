
namespace Slumber;

public class PlayerFallState : State
{
    protected Player p;
    public PlayerFallState(Player player) => p = player;

    public override void OnEnter()
    {

    }

    public override void Update(float delta)
    {
        p.AnimatedSprite.PlayAnimation("Fall", false);

        p.HandleMovementInput();
        p.HandleDeceleration();
        p.ApplyGravity();

        p.FlipSprite();

        if (Engine.Input.IsActionJustPressed("Jump"))
        {
            p.PlayerInfo.jumpBuffered = true;

            Engine.Timer.Wait(
                p.PlayerInfo.jumpBufferTime,
                () => p.PlayerInfo.jumpBuffered = false
            );
        }

        if (p.IsOnFloor && p.PlayerInfo.jumpBuffered)
        {
            p.PlayerInfo.jumpBuffered = false;
            RequestTransition(nameof(PlayerJumpState));
            return;
        }



        if (p.PlayerInfo.justLeftLedge)
            Engine.Timer.Wait(p.PlayerInfo.coyoteTimer, () => { p.PlayerInfo.justLeftLedge = false;});


        if (Engine.Input.IsActionJustPressed("Jump") && p.PlayerInfo.justLeftLedge)
        {
            RequestTransition(nameof(PlayerJumpState));
            return;
        }

        if (p.IsOnFloor)
        {
            RequestTransition(nameof(PlayerIdleState));
            return;
        }

        if (p.IsOnWall && p.PlayerAxis != 0)
        {
            RequestTransition(nameof(PlayerWallSlideState));
            return;
        }
    }
}

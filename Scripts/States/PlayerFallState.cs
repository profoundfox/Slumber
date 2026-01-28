
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


        if (p.PlayerInfo.justLeftLedge)
            Engine.Timer.Wait(p.PlayerInfo.coyoteTimer, () => { p.PlayerInfo.justLeftLedge = false;});


        if (Engine.Input.IsActionJustPressed("Jump") && p.PlayerInfo.justLeftLedge)
        {
            RequestTransition(nameof(PlayerJumpState));
        }

        if (p.IsOnGround())
        {
            RequestTransition(nameof(PlayerIdleState));
        }

        if (p.IsOnWall() && p.PlayerAxis != 0)
        {
            RequestTransition(nameof(PlayerWallSlideState));
        }
    }
}


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

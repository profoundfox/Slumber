
namespace Slumber;

public class PlayerJumpState : State
{
    protected Player p;
    private bool jumpReleased;

    public PlayerJumpState(Player player) => p = player;

    public override void OnEnter()
    {
        p.Velocity.Y = p.PlayerInfo.JumpForce;
        jumpReleased = false;
    }

    public override void Update(float delta)
    {
        p.AnimatedSprite.PlayAnimation("Fall", false);
        
        p.HandleMovementInput();
        p.ApplyGravity();

        p.FlipSprite();

        if (!jumpReleased && Engine.Input.IsActionJustReleased("Jump") && p.Velocity.Y < 0)
        {
            p.Velocity.Y /= 4f;
            jumpReleased = true;
        }

        if (p.Velocity.Y > 0)
        {
            RequestTransition(nameof(PlayerFallState));
            return;
        }

        if (p.IsOnGround())
        {
            RequestTransition(nameof(PlayerIdleState));
            return;
        }
    }
}

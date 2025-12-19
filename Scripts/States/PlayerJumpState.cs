
namespace Slumber;

public class PlayerJumpState : State
{
    protected Player p;
    private bool jumpReleased;

    public PlayerJumpState(Player player) => p = player;

    public override void OnEnter()
    {
        p.PlayerInfo.bufferActivated = false;
        p.Velocity.Y = p.PlayerInfo.JumpForce;
        jumpReleased = false;
    }

    public override void Update(GameTime gameTime)
    {
        p.AnimatedSprite.PlayAnimation("Fall", false);
        
        p.HandleMovementInput();
        p.ApplyGravity();


        if (!jumpReleased && Engine.Input.IsActionJustReleased("Jump") && p.Velocity.Y < 0)
        {
            p.Velocity.Y /= 4f;
            jumpReleased = true;
        }

        if ((Engine.Input.IsActionJustPressed("Jump") || p.PlayerInfo.bufferActivated) && !p.IsOnGround())
        {
            p.PlayerInfo.bufferActivated = true;
            CTimer.Wait(p.PlayerInfo.bufferTimer, () => p.PlayerInfo.bufferActivated = false);
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

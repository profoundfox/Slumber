
namespace Slumber.Entities;

public class PlayerGroundedState : State
{
    protected Player p;
    public PlayerGroundedState(Player player) => p = player;

    public override void Update(GameTime gameTime)
    {
        p.HandleHorizontalInput();

        if (!p.IsOnGround())
        {
            RequestTransition(nameof(PlayerFallState));
            return;
        }

        if (Engine.Input.IsActionJustPressed("Jump"))
        {
            RequestTransition(nameof(PlayerJumpState));
            return;
        }

        base.Update(gameTime);
    }
}

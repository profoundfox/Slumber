
namespace Slumber.Nodes;

public class PlayerFallState : State
{
    protected Player p;
    public PlayerFallState(Player player) => p = player;

    public override void OnEnter()
    {

    }

    public override void Update(GameTime gameTime)
    {
        p.AnimatedSprite.PlayAnimation("Fall", false);

        p.HandleHorizontalInput();

        if (p.IsOnGround() && p.PlayerInfo.bufferActivated == true)
        {
            RequestTransition(nameof(PlayerJumpState));
        }

        if (p.IsOnGround())
        {
            RequestTransition(nameof(PlayerIdleState));
        }

        if (p.IsOnWall())
        {
            RequestTransition(nameof(PlayerWallSlideState));
        }

        if ((Engine.Input.IsActionJustPressed("Jump") || p.PlayerInfo.bufferActivated) && !p.IsOnGround())
        {
            p.PlayerInfo.bufferActivated = true;
            CTimer.Wait(p.PlayerInfo.bufferTimer, () => p.PlayerInfo.bufferActivated = false);
        }
    }
}


namespace Slumber.Nodes;

public class PlayerWallSlideState : State
{
    protected Player p;
    public PlayerWallSlideState(Player player) => p = player;

    public override void OnEnter()
    {
        p.PlayerInfo.wallSlide = true;
        p.Velocity.Y = 0;
    }

    public override void Update(GameTime gameTime)
    {
        p.Velocity.Y = 0;
        
        if (!p.IsOnWall() || p.IsOnGround())
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

    public override void OnExit()
    {
        p.PlayerInfo.wallSlide = false;
    }
}

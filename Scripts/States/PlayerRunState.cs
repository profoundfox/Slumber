
namespace Slumber;

public class PlayerRunState : State
{
    public Player p;
    public PlayerRunState(Player player)
    {
        p = player;
    }

    public override void OnEnter()
    {
    }

    public override void Update(float delta)
    {
        base.Update(delta);

        p.HandleMovementInput();
        p.ApplyGravity();
        p.FlipSprite();

        
        p.AnimatedSprite.PlayAnimation("Run", false);

        if (p.PlayerAxis == 0)
        {
            RequestTransition(nameof(PlayerIdleState));
            return;
        }

        if (!p.IsOnFloor)
        {
            RequestTransition(nameof(PlayerFallState));
            p.PlayerInfo.justLeftLedge = true;
            return;
        }

        if (Engine.Input.IsActionJustPressed("Jump"))
        {
            RequestTransition(nameof(PlayerJumpState));
            return;
        }

        if (Engine.Input.IsActionJustPressed("Attack"))
        {
            RequestTransition(nameof(PlayerRunAttackState));
            return;
        }
    }
}

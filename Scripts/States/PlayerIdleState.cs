
namespace Slumber;

public class PlayerIdleState : State
{
    public Player p;

    public PlayerIdleState(Player player)
    {
        p = player;
    }


    public override void OnEnter()
    {
        
    }

    public override void Update(float delta)
    {
        p.AnimatedSprite.PlayAnimation("Idle", false);
        
        base.Update(delta);

        p.HandleDeceleration();
        p.ApplyGravity();

        if (p.PlayerAxis != 0)
        {
            RequestTransition(nameof(PlayerRunState));
            return;
        }

        if (Engine.Input.IsActionJustPressed("Jump"))
        {
            RequestTransition(nameof(PlayerJumpState));
            return;
        }

        if (Engine.Input.IsActionJustPressed("Attack"))
        {
            RequestTransition(nameof(PlayerAttackState));
            return;
        }
    }
}

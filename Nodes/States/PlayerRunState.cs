
namespace Slumber.Nodes;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(Player player) : base(player) { }

    public override void OnEnter()
    {
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        p.AnimatedSprite.PlayAnimation("Run", false);

        if (p.PlayerAxis == 0)
        {
            RequestTransition(nameof(PlayerIdleState));
            return;
        }

        if (Engine.Input.IsActionJustPressed("Attack"))
        {
            RequestTransition(nameof(PlayerAttackState));
            return;
        }
    }
}

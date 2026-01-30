
namespace Slumber;

public class PlayerAttackState : State
{
    public Player p;
    public PlayerAttackState(Player player)
    {
        p = player;
    }

    public override void OnEnter()
    {
        p.PlayerInfo.AttackCount++;
        p.PlayerInfo.attacking = true;

        if (p.PlayerInfo.AttackCount == 1)
            p.AnimatedSprite.PlayAnimation("Attack1", false);
        else
            p.AnimatedSprite.PlayAnimation("Attack2", false);
    }

    public override void Update(float delta)
    {
        base.Update(delta);

        p.HandleMovementInput();
        p.HandleDeceleration();
        p.FlipSprite();
        p.ApplyGravity();

        if (p.AnimatedSprite.Finished)
        {
            p.PlayerInfo.attacking = false;
            RequestTransition(nameof(PlayerIdleState));
        }

        if (Engine.Input.IsActionJustPressed("Jump"))
        {
            RequestTransition(nameof(PlayerJumpState));
            return;
        }
    }

    public override void OnExit()
    {
        p.PlayerInfo.attacking = false;
        //p.DamageArea.Enabled = false;
    }
}

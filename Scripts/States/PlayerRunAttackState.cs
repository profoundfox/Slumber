namespace Slumber 
{
    public class PlayerRunAttackState : State
    {
        public Player p;
        public PlayerRunAttackState(Player player)
        {
            p = player;
        }

        public override void OnEnter()
        {
            p.PlayerInfo.AttackCount++;
            p.PlayerInfo.attacking = true;

            p.AnimatedSprite.PlayAnimation("RunAttack");
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
}
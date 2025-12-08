
namespace Slumber.Nodes
{
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
            p.HandleHorizontalInput();

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
        }
    }
}

namespace Slumber;

public class JumpState : BaseAirState
{
  Player p => Core.Token.Get<Player>();

  public override void OnEnter()
  {
    base.OnEnter();

    Jump();
  }

  public override void OnExit()
  {
    base.OnExit();
  }

  public override void Update(float delta)
  {
    base.Update(delta);
    
    p.Sprite.PlayAnimation("Fall");

    if (p.Velocity.Y >= 0)
      Transition?.Invoke("FallState");
  }

  public override void Physics(float delta)
  {
    base.Physics(delta);
    
    if (Main.LeftKey != null && Main.RightKey != null && Main.DownKey != null && Main.UpKey != null)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.LeftKey.Value, Main.RightKey.Value, Main.DownKey.Value, Main.UpKey.Value).ToVector2();

    HandleJump();
  }

  public void Jump()
  {
    p.Velocity.Y = p.Properties.JumpForce;
    p.Properties.JumpReleased = false;
    p.Properties.CanCoyoteJump = false;
    p.Properties.JumpBuffered = false;
  }

  public void HandleJump()
  {
    if (!p.Properties.JumpReleased && !((Main.JumpKey != null && Core.Input.Keyboard.IsDown(Main.JumpKey.Value)
        || Main.JumpButton != null && Core.Input.CurrentGamePad.IsDown(Main.JumpButton.Value)
        || Main.JumpMouse != null && Core.Input.Mouse.IsDown(Main.JumpMouse.Value)) || p.Properties.JumpBuffered) && p.Velocity.Y < 0)
    {
      p.Velocity.Y /= 2f;
      p.Properties.JumpReleased = true;
    }
  }
}

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
    
    if (Main.Binds.LeftKey != null && Main.Binds.RightKey != null && Main.Binds.DownKey != null && Main.Binds.UpKey != null)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey.Value, Main.Binds.RightKey.Value, Main.Binds.DownKey.Value, Main.Binds.UpKey.Value).ToVector2();

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
    if (!p.Properties.JumpReleased && !((Main.Binds.JumpKey != null && Core.Input.Keyboard.IsDown(Main.Binds.JumpKey.Value)
        || Main.Binds.JumpButton != null && Core.Input.CurrentGamePad.IsDown(Main.Binds.JumpButton.Value)
        || Main.Binds.JumpMouse != null && Core.Input.Mouse.IsDown(Main.Binds.JumpMouse.Value)) || p.Properties.JumpBuffered) && p.Velocity.Y < 0)
    {
      p.Velocity.Y /= 2f;
      p.Properties.JumpReleased = true;
    }
  }
}

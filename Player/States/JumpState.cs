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
    
    if (Main.Binds.LeftKey != Keys.None && Main.Binds.RightKey != Keys.None && Main.Binds.DownKey != Keys.None && Main.Binds.UpKey != Keys.None)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey, Main.Binds.RightKey, Main.Binds.DownKey, Main.Binds.UpKey).ToVector2();

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
    if (!p.Properties.JumpReleased && !((Main.Binds.JumpKey != Keys.None && Core.Input.Keyboard.IsDown(Main.Binds.JumpKey)
        || Main.Binds.JumpButton != Buttons.None && Core.Input.CurrentGamePad.IsDown(Main.Binds.JumpButton)
        || Main.Binds.JumpMouse != MouseButton.None && Core.Input.Mouse.IsDown(Main.Binds.JumpMouse)) || p.Properties.JumpBuffered) && p.Velocity.Y < 0)
    {
      p.Velocity.Y /= 2f;
      p.Properties.JumpReleased = true;
    }
  }
}

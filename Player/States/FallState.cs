namespace Slumber;

public class FallState : BaseAirState
{
  Player p => Core.Token.Get<Player>();

  public override void OnEnter()
  {
    base.OnEnter();
  }

  public override void OnExit()
  {
    base.OnExit();
  }

  public override void Physics(float delta)
  {
    base.Physics(delta);

    if (Main.Binds.LeftKey != Keys.None && Main.Binds.RightKey != Keys.None && Main.Binds.DownKey != Keys.None && Main.Binds.UpKey != Keys.None)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey, Main.Binds.RightKey, Main.Binds.DownKey, Main.Binds.UpKey).ToVector2();

    if (Main.Binds.JumpKey != Keys.None && Core.Input.Keyboard.WasJustPressed(Main.Binds.JumpKey)
        || Main.Binds.JumpMouse != MouseButton.None && Core.Input.Mouse.WasJustPressed(Main.Binds.JumpMouse) 
        || Main.Binds.JumpButton != Buttons.None && Core.Input.CurrentGamePad.WasJustPressed(Main.Binds.JumpButton))
    {
      if (p.Properties.CanCoyoteJump)
      {
        Transition?.Invoke("JumpState");
      }

      p.Properties.JumpBuffered = true;
      Await.Span(p.Properties.JumpBufferTime, () => p?.Properties.JumpBuffered = false);
    }
  }

  public override void Update(float delta)
  {
    base.Update(delta);

    p.Sprite.PlayAnimation("Fall");

    if (p.IsOnFloor)
      Transition?.Invoke("IdleState");

    if (p.CanWall())
      Transition?.Invoke("WallSlideState");
  }
}

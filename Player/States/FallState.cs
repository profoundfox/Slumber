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

    if (Main.Binds.LeftKey != null && Main.Binds.RightKey != null && Main.Binds.DownKey != null && Main.Binds.UpKey != null)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey.Value, Main.Binds.RightKey.Value, Main.Binds.DownKey.Value, Main.Binds.UpKey.Value).ToVector2();

    if (Main.Binds.JumpKey != null && Core.Input.Keyboard.WasJustPressed(Main.Binds.JumpKey.Value)
        || Main.Binds.JumpMouse != null && Core.Input.Mouse.WasJustPressed(Main.Binds.JumpMouse.Value) 
        || Main.Binds.JumpButton != null && Core.Input.CurrentGamePad.WasJustPressed(Main.Binds.JumpButton.Value))
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

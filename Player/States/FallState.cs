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

    if (Main.LeftKey != null && Main.RightKey != null && Main.DownKey != null && Main.UpKey != null)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.LeftKey.Value, Main.RightKey.Value, Main.DownKey.Value, Main.UpKey.Value).ToVector2();

    if (Main.JumpKey != null && Core.Input.Keyboard.WasJustPressed(Main.JumpKey.Value)
        || Main.JumpMouse != null && Core.Input.Mouse.WasJustPressed(Main.JumpMouse.Value) 
        || Main.JumpButton != null && Core.Input.CurrentGamePad.WasJustPressed(Main.JumpButton.Value))
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

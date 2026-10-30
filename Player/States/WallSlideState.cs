namespace Slumber;

public class WallSlideState : State
{
  Player p => Core.Token.Get<Player>();

  public override void OnEnter()
  {
    base.OnEnter();
  }

  public override void Update(float delta)
  {
    base.Update(delta);

    if (!p.IsOnWall || p.IsOnFloor)
      Transition?.Invoke("FallState");
  }

  public override void Physics(float delta)
  {
    base.Physics(delta);
    
    if (Main.LeftKey != null && Main.RightKey != null && Main.DownKey != null && Main.UpKey != null)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.LeftKey.Value, Main.RightKey.Value, Main.DownKey.Value, Main.UpKey.Value).ToVector2();

    p.HandleMovementInput();

    p.Velocity.Y = MathF.Min(
      p.Velocity.Y + p.Properties.WallSlideGravity,
      p.Properties.WallSlideGravity
    );

    if (Main.JumpKey != null && Core.Input.Keyboard.WasJustPressed(Main.JumpKey.Value)
        || Main.JumpButton != null && Core.Input.CurrentGamePad.WasJustPressed(Main.JumpButton.Value)
        || Main.JumpMouse != null && Core.Input.Mouse.WasJustPressed(Main.JumpMouse.Value))
      p.WallJump();
  }
}

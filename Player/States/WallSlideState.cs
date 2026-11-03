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
    
    if (Main.Binds.LeftKey != null && Main.Binds.RightKey != null && Main.Binds.DownKey != null && Main.Binds.UpKey != null)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey.Value, Main.Binds.RightKey.Value, Main.Binds.DownKey.Value, Main.Binds.UpKey.Value).ToVector2();

    p.HandleMovementInput();

    p.Velocity.Y = MathF.Min(
      p.Velocity.Y + p.Properties.WallSlideGravity,
      p.Properties.WallSlideGravity
    );

    if (Main.Binds.JumpKey != null && Core.Input.Keyboard.WasJustPressed(Main.Binds.JumpKey.Value)
        || Main.Binds.JumpButton != null && Core.Input.CurrentGamePad.WasJustPressed(Main.Binds.JumpButton.Value)
        || Main.Binds.JumpMouse != null && Core.Input.Mouse.WasJustPressed(Main.Binds.JumpMouse.Value))
      p.WallJump();
  }
}

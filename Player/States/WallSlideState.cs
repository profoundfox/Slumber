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
    
    if (Main.Binds.LeftKey != Keys.None && Main.Binds.RightKey != Keys.None && Main.Binds.DownKey != Keys.None && Main.Binds.UpKey != Keys.None)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey, Main.Binds.RightKey, Main.Binds.DownKey, Main.Binds.UpKey).ToVector2();

    p.HandleMovementInput();

    p.Velocity.Y = MathF.Min(
      p.Velocity.Y + p.Properties.WallSlideGravity,
      p.Properties.WallSlideGravity
    );

    if (Main.Binds.JumpKey != Keys.None && Core.Input.Keyboard.WasJustPressed(Main.Binds.JumpKey)
        || Main.Binds.JumpButton != Buttons.None && Core.Input.CurrentGamePad.WasJustPressed(Main.Binds.JumpButton)
        || Main.Binds.JumpMouse != MouseButton.None && Core.Input.Mouse.WasJustPressed(Main.Binds.JumpMouse))
      p.WallJump();
  }
}

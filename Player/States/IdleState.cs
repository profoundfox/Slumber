namespace Slumber;

public class IdleState : State
{
  Player p => Core.Token.Get<Player>();

  public override void OnEnter()
  {

  }

  public override void OnExit()
  {

  }

  public override void Update(float delta)
  {
    p.Sprite.PlayAnimation("Idle");

    if (p.Properties.PlayerAxis.X != 0) 
      Transition?.Invoke("RunState");

    if ((Main.Binds.JumpKey != Keys.None && Core.Input.Keyboard.WasJustPressed(Main.Binds.JumpKey)
        || Main.Binds.JumpButton != Buttons.None && Core.Input.CurrentGamePad.WasJustPressed(Main.Binds.JumpButton)
        || Main.Binds.JumpMouse != MouseButton.None && Core.Input.Mouse.WasJustPressed(Main.Binds.JumpMouse)) || p.Properties.JumpBuffered)
      Transition?.Invoke("JumpState");

    if (!p.IsOnFloor)
      Transition?.Invoke("FallState");

    if (Main.Binds.AttackKey != Keys.None && Core.Input.Keyboard.WasJustPressed(Main.Binds.AttackKey)
        || Main.Binds.AttackButton != Buttons.None && Core.Input.CurrentGamePad.WasJustPressed(Main.Binds.AttackButton)
        || Main.Binds.AttackMouse != MouseButton.None && Core.Input.Mouse.WasJustPressed(Main.Binds.AttackMouse))
      Transition?.Invoke("FloorAttackState");
  }

  public override void Physics(float delta)
  {
    if (Main.Binds.LeftKey != Keys.None && Main.Binds.RightKey != Keys.None && Main.Binds.DownKey != Keys.None && Main.Binds.UpKey != Keys.None)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey, Main.Binds.RightKey, Main.Binds.DownKey, Main.Binds.UpKey).ToVector2();

    p.HandleDeceleration(delta);
    p.HandleCoyoteTime();
  }
}

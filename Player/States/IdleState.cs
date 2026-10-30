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

    if ((Main.JumpKey != null && Core.Input.Keyboard.WasJustPressed(Main.JumpKey.Value)
        || Main.JumpButton != null && Core.Input.CurrentGamePad.WasJustPressed(Main.JumpButton.Value)
        || Main.JumpMouse != null && Core.Input.Mouse.WasJustPressed(Main.JumpMouse.Value)) || p.Properties.JumpBuffered)
      Transition?.Invoke("JumpState");

    if (!p.IsOnFloor)
      Transition?.Invoke("FallState");

    if (Main.AttackKey != null && Core.Input.Keyboard.WasJustPressed(Main.AttackKey.Value)
        || Main.AttackButton != null && Core.Input.CurrentGamePad.WasJustPressed(Main.AttackButton.Value)
        || Main.AttackMouse != null && Core.Input.Mouse.WasJustPressed(Main.AttackMouse.Value))
      Transition?.Invoke("FloorAttackState");
  }

  public override void Physics(float delta)
  {
    if (Main.LeftKey != null && Main.RightKey != null && Main.DownKey != null && Main.UpKey != null)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.LeftKey.Value, Main.RightKey.Value, Main.DownKey.Value, Main.UpKey.Value).ToVector2();

    p.HandleDeceleration(delta);
    p.HandleCoyoteTime();
  }
}

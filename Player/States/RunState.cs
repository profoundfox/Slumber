namespace Slumber;

public class RunState : State
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
    p.Sprite.PlayAnimation("Run");
    
    if (p.Properties.PlayerAxis.X == 0)
      Transition?.Invoke("IdleState"); 
    if (Main.Binds.JumpKey != null && Core.Input.Keyboard.WasJustPressed(Main.Binds.JumpKey.Value)
        || Main.Binds.JumpButton != null && Core.Input.CurrentGamePad.WasJustPressed(Main.Binds.JumpButton.Value)
        || Main.Binds.JumpMouse != null && Core.Input.Mouse.WasJustPressed(Main.Binds.JumpMouse.Value))
      Transition?.Invoke("JumpState");
    if (!p.IsOnFloor)
      Transition?.Invoke("FallState");
    if (p.CanWall())
      Transition?.Invoke("WallSlideState");
    if (Main.Binds.AttackKey != null && Core.Input.Keyboard.WasJustPressed(Main.Binds.AttackKey.Value)
        || Main.Binds.AttackButton != null && Core.Input.CurrentGamePad.WasJustPressed(Main.Binds.AttackButton.Value)
        || Main.Binds.AttackMouse != null && Core.Input.Mouse.WasJustPressed(Main.Binds.AttackMouse.Value))
      Transition?.Invoke("FloorAttackState");
  }

  public override void Physics(float delta)
  {
    if (Main.Binds.LeftKey != null && Main.Binds.RightKey != null && Main.Binds.DownKey != null && Main.Binds.UpKey != null)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey.Value, Main.Binds.RightKey.Value, Main.Binds.DownKey.Value, Main.Binds.UpKey.Value).ToVector2();

    p.HandleMovementInput();
    p.HandleCoyoteTime();
  }
}

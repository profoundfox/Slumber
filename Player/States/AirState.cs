namespace Slumber;

public class BaseAirState : State
{
  Player p => Core.Token.Get<Player>();

  public override void Physics(float delta)
  {
    base.Physics(delta);
    
    if (Main.LeftKey != null && Main.RightKey != null && Main.DownKey != null && Main.UpKey != null)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.LeftKey.Value, Main.RightKey.Value, Main.DownKey.Value, Main.UpKey.Value).ToVector2();

    p.HandleMovementInput();
    p.HandleDeceleration(delta);
    p.HandleCoyoteTime();
    p.ApplyGravity(delta);
  }
}

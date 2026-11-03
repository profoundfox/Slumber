namespace Slumber;

public class BaseAirState : State
{
  Player p => Core.Token.Get<Player>();

  public override void Physics(float delta)
  {
    base.Physics(delta);
    
    if (Main.Binds.LeftKey != null && Main.Binds.RightKey != null && Main.Binds.DownKey != null && Main.Binds.UpKey != null)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey.Value, Main.Binds.RightKey.Value, Main.Binds.DownKey.Value, Main.Binds.UpKey.Value).ToVector2();

    p.HandleMovementInput();
    p.HandleDeceleration(delta);
    p.HandleCoyoteTime();
    p.ApplyGravity(delta);
  }
}

namespace Slumber;

public class BaseAirState : State
{
  Player p => Core.Token.Get<Player>();

  public override void Physics(float delta)
  {
    base.Physics(delta);
    
    if (Main.Binds.LeftKey != Keys.None && Main.Binds.RightKey != Keys.None && Main.Binds.DownKey != Keys.None && Main.Binds.UpKey != Keys.None)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey, Main.Binds.RightKey, Main.Binds.DownKey, Main.Binds.UpKey).ToVector2();

    p.HandleMovementInput();
    p.HandleDeceleration(delta);
    p.HandleCoyoteTime();
    p.ApplyGravity(delta);
  }
}

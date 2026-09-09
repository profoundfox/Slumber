namespace Slumber;

public class BaseAirState : State
{
  Player p => Core.Token.Get<Player>();

  public override void Physics(float delta)
  {
    base.Physics(delta);

    p.Properties.PlayerAxis = Core.Input.GetAxis("MoveLeft", "MoveRight", "MoveDown", "MoveUp").ToVector2();

    p.HandleMovementInput();
    p.HandleDeceleration(delta);
    p.HandleCoyoteTime();
    p.ApplyGravity(delta);
  }
}

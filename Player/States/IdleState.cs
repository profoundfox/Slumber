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

    if (Core.Input.IsActionJustPressed("Jump") || p.Properties.JumpBuffered)
      Transition?.Invoke("JumpState");

    if (!p.IsOnFloor)
      Transition?.Invoke("FallState");

    if (Core.Input.IsActionJustPressed("Attack"))
      Transition?.Invoke("FloorAttackState");
  }

  public override void Physics(float delta)
  {
    p.Properties.PlayerAxis = Core.Input.GetAxis("MoveLeft", "MoveRight", "MoveDown", "MoveUp").ToVector2();

    p.HandleDeceleration(delta);
    p.HandleCoyoteTime();
  }
}

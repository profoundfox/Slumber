
namespace Slumber;

public class FloorAttackState : State
{
  Player p => Core.Token.Get<Player>();

  bool pushedBack;

  public override void OnEnter()
  {
    p.Velocity.X = 0;
    p.Sprite.PlayAnimation("Attack");
    Attack();
  }

  public override void Update(float delta)
  {
    if (Core.Input.IsActionJustPressed("Attack"))
      BufferAttack();

    if (p.Sprite.IsFinished)
    {
      p.AttackArea.Get<CollisionShape2D>().Disabled = true;
      p.Properties.IsAttacking = false;
      pushedBack = false;

      if (p.Properties.AttackBuffer)
      {
        p.Properties.AttackBuffer = false;
        Attack();
      }
      else
        Transition?.Invoke("IdleState");
    }
  }

  public void Attack()
  {
    p.AttackArea.Get<CollisionShape2D>().Disabled = false;
    p.Properties.AttackCounter++;
    p.Properties.IsAttacking = true;
  }

  public void BufferAttack()
  {
    if (p.Properties.AttackBuffer)
      return;

    p.Properties.AttackBuffer = true;

    Await.Span(p.Properties.AttackBufferTime, () =>
    {
      p.Properties.AttackBuffer = false;
    });
  }

  public override void Physics(float delta)
  {
    p.Properties.PlayerAxis = Core.Input.GetAxis("MoveLeft", "MoveRight", "MoveDown", "MoveUp").ToVector2();

    if (p.AttackArea.IsInsideAnyBody() && !pushedBack)
    {
      p.Velocity.X = -500;
      pushedBack = true;
    }
    
    //p.HandleMovementInput();
    p.HandleDeceleration(delta);
    p.ApplyGravity(delta);
    p.HandleCoyoteTime();
  }
}

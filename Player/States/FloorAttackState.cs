
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
    if (Main.Binds.AttackKey != null && Core.Input.Keyboard.WasJustPressed(Main.Binds.AttackKey.Value)
        || Main.Binds.AttackButton != null && Core.Input.CurrentGamePad.WasJustPressed(Main.Binds.AttackButton.Value)
        || Main.Binds.AttackMouse != null && Core.Input.Mouse.WasJustPressed(Main.Binds.AttackMouse.Value))
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
    if (Main.Binds.LeftKey != null && Main.Binds.RightKey != null && Main.Binds.DownKey != null && Main.Binds.UpKey != null)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey.Value, Main.Binds.RightKey.Value, Main.Binds.DownKey.Value, Main.Binds.UpKey.Value).ToVector2();

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

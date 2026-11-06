
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
    if (Main.Binds.AttackKey != Keys.None && Core.Input.Keyboard.WasJustPressed(Main.Binds.AttackKey)
        || Main.Binds.AttackButton != Buttons.None && Core.Input.CurrentGamePad.WasJustPressed(Main.Binds.AttackButton)
        || Main.Binds.AttackMouse != MouseButton.None && Core.Input.Mouse.WasJustPressed(Main.Binds.AttackMouse))
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
    if (Main.Binds.LeftKey != Keys.None && Main.Binds.RightKey != Keys.None && Main.Binds.DownKey != Keys.None && Main.Binds.UpKey != Keys.None)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey, Main.Binds.RightKey, Main.Binds.DownKey, Main.Binds.UpKey).ToVector2();

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

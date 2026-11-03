namespace Slumber;

public class LandingState : State
{
  Player p => Core.Token.Get<Player>();

  Vector2 enterVel;

  public override void OnEnter()
  {
    base.OnEnter();
    if (p.Properties.ThresholdReached)
    {
      p.Sprite.PlayAnimation("Landing");
    }
    else
    {
      Transition?.Invoke("IdleState");
      return;
    }

    enterVel = p.Velocity;

    p.Velocity = Vector2.Zero;

    p.Properties.ThresholdReached = false;

    Core.Token.Get<PixelCamera>()?.Shake(() => p.Sprite.IsFinished, 10, 9f);
  }

  public override void Update(float delta)
  {
    base.Update(delta);

    if (p.Sprite.IsFinished)
    {
      Transition?.Invoke("IdleState");
    }
  }

  public override void Physics(float delta)
  {
    base.PhysicsUpdate(delta);

    if (Main.Binds.LeftKey != null && Main.Binds.RightKey != null && Main.Binds.DownKey != null && Main.Binds.UpKey != null)
      p.Properties.PlayerAxis = Core.Input.Keyboard.GetAxis(Main.Binds.LeftKey.Value, Main.Binds.RightKey.Value, Main.Binds.DownKey.Value, Main.Binds.UpKey.Value).ToVector2();

    p.HandleCoyoteTime();
  }
}

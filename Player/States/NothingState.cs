
namespace Slumber;

public class NothingState : State
{
  Player p => Core.Token.Get<Player>();

  public override void OnEnter()
  {
    base.OnEnter();
  }

  public override void Update(float delta)
  {
    base.Update(delta);
  }

  public override void Physics(float delta)
  {
    base.PhysicsUpdate(delta);
  }
}

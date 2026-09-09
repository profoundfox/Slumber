
using System.Linq;

namespace Slumber;

public class TransitionState : State
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
  }

  public override void Physics(float delta)
  {
    p.Velocity.X = 0;
    p.ApplyGravity(delta);
  }
}


using System.Linq;

namespace Slumber;

public class TransitionState : State
{
  Player p => Core.Token.Get<Player>();

  public override void OnEnter()
  {
    Core.Token.Get<PixelCamera>().FollowX = false;
    Core.Token.Get<PixelCamera>().FollowY = false;
  }

  public override void OnExit()
  {
    p.Velocity = Vector2.Zero;

    Core.Token.Get<PixelCamera>().FollowX = true;
    Core.Token.Get<PixelCamera>().FollowY = true;
  }

  public override void Update(float delta)
  {
    p.Properties.PlayerAxis = Vector2.Zero;
    
    if (p.IsOnFloor)
      p.STM.ChangeState("IdleState");
    else
      p.Sprite.PlayAnimation("Fall");
  }

  public override void Physics(float delta)
  {
    p.ApplyGravity(delta);
  }
}

using System;
using ConstructEngine;
using ConstructEngine.Util;
using Microsoft.Xna.Framework;

namespace Slumber.Entities;

public class PlayerWallJumpState : State
{
    private readonly Player p;
    private float controlLockTimer = 0.06f;
    private bool controlRestored;

    public PlayerWallJumpState(Player player) => p = player;

    public override void OnEnter()
    {

        p.PlayerInfo.canMove = false;
        controlRestored = false;

        if (p.PlayerInfo.dir == 1)
            p.KinematicBase.Velocity.X = -p.PlayerInfo.WallJumpHorizontalSpeed;
        
        else
            p.KinematicBase.Velocity.X = p.PlayerInfo.WallJumpHorizontalSpeed;

        p.KinematicBase.Velocity.Y = -p.PlayerInfo.WallJumpVerticalSpeed;


    }

    public override void Update(GameTime gameTime)
    {
        
        if (!controlRestored)
        {
            Timer.Wait(controlLockTimer, () => { controlRestored = true; p.PlayerInfo.canMove = true; });
        }

        if (controlRestored)
            p.HandleHorizontalInput();

        if (p.KinematicBase.Velocity.Y > 0)
            RequestTransition(nameof(PlayerFallState));

        if (p.KinematicBase.IsOnGround())
            RequestTransition(nameof(PlayerIdleState));
    }

    public override void OnExit()
    {
        p.PlayerInfo.canMove = true;
        controlRestored = true;
    }
}

using System;
using ConstructEngine;
using Microsoft.Xna.Framework;
using Slumber.Entities;

public class FallState : State
{

    Player player;

    public FallState (Player player)
    {
        this.player = player;
    }

    public override void OnEnter()
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        if (Core.Input.Keyboard.IsKeyDown(player.MoveLeftKey) || Core.Input.Keyboard.IsKeyDown(player.MoveRightKey))
        {
            RequestTransition("FallMoveState");
        }

        if (player.KinematicBase.Velocity.Y == 0)
        {
            RequestTransition("IdleState");
        }
    }
    
    public override void OnExit()
    {
        
    }
}
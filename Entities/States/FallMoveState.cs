using System;
using ConstructEngine;
using Microsoft.Xna.Framework;
using Slumber.Entities;

public class FallMoveState : State
{

    Player player;

    public FallMoveState (Player player)
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

        }

        else
        {
            RequestTransition("FallState");
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
using System;
using ConstructEngine;
using Microsoft.Xna.Framework;
using Slumber.Entities;

public class JumpState : State
{

    Player player;

    public JumpState (Player player)
    {
        this.player = player;
    }

    public override void OnEnter()
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        if (player.KinematicBase.Velocity.Y < 0)
        {
            if (Core.Input.Keyboard.IsKeyDown(player.MoveLeftKey) || Core.Input.Keyboard.IsKeyDown(player.MoveRightKey))
            {
                RequestTransition("FallMoveState");
            }
            else
            {
                RequestTransition("FallState");
            }
        }
    }
    
    public override void OnExit()
    {
        
    }
}
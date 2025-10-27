using System;
using System.ComponentModel;
using System.Net.Mime;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Timers;
using ConstructEngine;
using ConstructEngine.Components.Entity;
using ConstructEngine.Components.Physics;
using ConstructEngine.Graphics;
using ConstructEngine.Gum;
using ConstructEngine.Physics;
using ConstructEngine.Util;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Slumber.Screens;
using Timer = ConstructEngine.Util.Timer;

namespace Slumber.Entities;

public class Player : Entity, Entity.IEntity
{
 
    private bool attacking;
    
    public float Gravity = 1300f;
    public float JumpForce = -350f;
    public float MoveSpeed = 100f;


    private float bufferTimer = 0.2f;
    private float WallJumpHorizontalSpeed = 200;
    private float WallJumpVerticalSpeed = 300;

    
    
    private bool bufferActivated;
    private bool dir = true;
    private int AttackCount = 0;
    private int textureOffset;
    private bool _upwardAir;
    private bool canMove = true;
    private bool wallSlide;
    private bool falling;
    
    private TextureAtlas _atlas;
    private TextureAtlas _atlasFeet;

    
    Animation _runAnim;
    Animation _idleAnim;
    Animation _fallAnim;

    
    private Vector2 AnimatedSpriteRenderingPosition;

    Keys MoveRightKey = Keys.Right;
    Keys MoveLeftKey = Keys.Left;
    Keys JumpKey = Keys.Space;
    Keys AttackKey = Keys.X;

    int AttackColliderOffset;

    Collider DamageArea;

    HealthComponent HealthComponent;



    private PlayerUI Screen;
    private PauseMenu pauseMenu;

    public Player() : base(4)
    {
    }

    public override void Load()
    {
        Screen = new PlayerUI();
        pauseMenu = new PauseMenu();
        GumHelper.AddScreenToRoot(pauseMenu);
        GumHelper.AddScreenToRoot(Screen);


        _atlas = TextureAtlas.FromFile(Core.Content, "Assets/Atlas/Player/player-atlas.xml", "Assets/Animations/Player/PlayerModel3Atlas");
                
        _runAnim = _atlas.CreateAnimatedSprite("run-animation").Animation;
        _idleAnim = _atlas.CreateAnimatedSprite("idle-animation").Animation;
        _fallAnim = _atlas.CreateAnimatedSprite("fall-animation").Animation;
    

        AnimatedSprite = _atlas.CreateAnimatedSprite("idle-animation");
        AnimatedSprite.LayerDepth = 0.5f;

        KinematicBase.Collider = new Collider(new Rectangle(400, 150, 10, 25), true, this);


        Circle AttackCircle = new(0, 0, 30);


        DamageArea = new Collider(AttackCircle, false, this);

        HealthComponent = new HealthComponent(this, 5, KinematicBase.Collider);
        
    
    }

    public void Update(GameTime gameTime)
    {
        if (KinematicBase.Velocity.Y < 0)
        {
            falling = false;
        }
        else
        {
            falling = true;
        }

        HealthComponent.Update(gameTime);


        if (Core.Input.Keyboard.WasKeyJustPressed(Keys.Escape))
        {
            
            Core.SceneManager.SceneFrozen = true;
            pauseMenu.Root.Visible = true;
        }

        DamageArea.Circ.X = KinematicBase.Collider.Rect.X + AttackColliderOffset;
        DamageArea.Circ.Y = KinematicBase.Collider.Rect.Y - 10;

        
        Screen.LabelInstance.Text = "Health: " + HealthComponent.CurrentHealth + " Colliding: " + KinematicBase.Collider.IsIntersectingAny() + " Type: "  + KinematicBase.Collider.GetCurrentlyIntersectingCollider()?.GetType();
        
        HandleInput(gameTime);

        HandleGravity(gameTime);
        
        KinematicBase.UpdateCollider(gameTime);
            
        if (KinematicBase.IsOnGround())
        {
            KinematicBase.Velocity.Y = 0;
        }
        
        AnimatedSpriteRenderingPosition = new Vector2(KinematicBase.Collider.Rect.X - 45, KinematicBase.Collider.Rect.Y - 39);
        
        FlipSprite();
        HandleWall();
        HandleWallJump();
        Animation();
        
        AnimatedSprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawSprites(spriteBatch, AnimatedSpriteRenderingPosition, textureOffset);

        //ColliderDraw.DrawCircle(AttackCollider.Circ, Color.Red, 2);
        //DrawHelper.DrawRectangle(KinematicBase.Collider.Rect, Color.Red, 2, 0.6f);
    }
    
    //Functions
    private void HandleWall()
    {
        if (KinematicBase.IsOnWall())
        {
            wallSlide = true;
        }

        if (!KinematicBase.IsOnWall() || KinematicBase.IsOnGround() || KinematicBase.Velocity.Y < 0)
        {
            wallSlide = false;
        }
        
        if (wallSlide)
        {
            KinematicBase.Velocity.Y = 0;
        }
    }

    private void HandleWallJump()
    {
        if (KinematicBase.IsOnWall() && !KinematicBase.IsOnGround() && wallSlide)
        {
            if (Core.Input.Keyboard.WasKeyJustPressed(JumpKey))
            {
                canMove = false;

                if (dir)
                {
                    KinematicBase.Velocity.X = -WallJumpHorizontalSpeed;
                }
                else
                {
                    KinematicBase.Velocity.X = WallJumpHorizontalSpeed;
                }

                KinematicBase.Velocity.Y = -WallJumpVerticalSpeed;

                Timer.Wait(0.12f, () => { canMove = true; });
            }
        }
    }

    private void HandleGravity(GameTime gameTime)
    {
        KinematicBase.Velocity.Y += Gravity * Core.DeltaTime;
    }

    private void HandleInput(GameTime gameTime)
    {
        if (canMove) 
        {
            KinematicBase.Velocity.X = 0;
            
            if (Core.Input.Keyboard.IsKeyDown(MoveLeftKey) && !Core.Input.Keyboard.IsKeyDown(MoveRightKey))
            {
                KinematicBase.Velocity.X = -MoveSpeed;
                dir = false;
            }

            if (Core.Input.Keyboard.IsKeyDown(MoveRightKey) && !Core.Input.Keyboard.IsKeyDown(MoveLeftKey))
            { 
                KinematicBase.Velocity.X = MoveSpeed;
                dir = true;
            }
        }
        
        //HandleAttack();

        if (AttackCount == 3)
        {
            AttackCount = 0;
        }
        
        Jump(gameTime);
    }

    private void HandleAttack()
    {
        if (!attacking)
        {
            if (Core.Input.Keyboard.WasKeyJustPressed(AttackKey))
            {
                AttackCount++;

                attacking = true;

                DamageArea.Enabled = true;

            }
        }

        else
        {
            if (AnimatedSprite.finished)
            {
                attacking = false;
                DamageArea.Enabled = false;
            }
        }
    }

    private void Jump(GameTime gameTime)
    {
        if (Core.Input.Keyboard.WasKeyJustPressed(JumpKey) || bufferActivated)
        {
            if (KinematicBase.IsOnGround())
            {
                _upwardAir = true;
                bufferActivated = false;
                KinematicBase.Velocity.Y = JumpForce;
            }
            else
            {
                bufferActivated = true;
                Timer.Wait(bufferTimer, () => bufferActivated = false);
            }
        }

        if (Core.Input.Keyboard.WasKeyJustReleased(JumpKey) && KinematicBase.Velocity.Y < 0)
        {
            KinematicBase.Velocity.Y = (JumpForce / 4);
        }

        if (_upwardAir && KinematicBase.Velocity.Y > 0)
        {
            _upwardAir = false;
        }
    }

    private void FlipSprite()
    {
        if (dir)
        {
            AttackColliderOffset = 15;
            textureOffset = 0;
            AnimatedSprite.Effects = SpriteEffects.None;
        }
        else
        {
            AttackColliderOffset = -38;
            textureOffset = 4;
            AnimatedSprite.Effects = SpriteEffects.FlipHorizontally;
        }
    }
    
    private void Animation()
    {
        if (!attacking)
        {
            if (KinematicBase.IsOnGround())
            {
                if (KinematicBase.Velocity != Vector2.Zero && !KinematicBase.IsOnWall())
                {
                    AnimatedSprite.PlayAnimation(_runAnim, false);
                }
                else
                {
                    AnimatedSprite.PlayAnimation(_idleAnim, false);
                }
            }
            
            else
            {
                AnimatedSprite.PlayAnimation(_fallAnim, false);
            }
        }
    }
}


using System;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using ConstructEngine;
using ConstructEngine.Components.Entity;
using ConstructEngine.Graphics;
using ConstructEngine.Objects;
using ConstructEngine.Physics;
using ConstructEngine.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Timer = ConstructEngine.Util.Timer;

namespace Slumber.Entities;

public class Enemy : Entity, Entity.IEntity
{
    public float Gravity = 1300f;

    private int Health;
    private int Direction = 1;
    private int TextureOffset;

    private Ray2D EnemyRay;

    private Vector2 SpritePosition;

    private bool CanTakeDamage = true;
    private bool RayColliding;

    private TextureAtlas Atlas;

    private Texture2D AltasTexture;

    private Animation RunAnimation;

    public Enemy() : base(1)
    {

    }


    public override void Load()
    {
        Atlas = TextureAtlas.FromFile(Core.Content, "Assets/Atlas/enemyatlas.xml", "Assets/Animations/Enemies/grassspidersheet");

        RunAnimation = Atlas.CreateAnimatedSprite("run-animation").Animation;

        AnimatedSprite = Atlas.CreateAnimatedSprite("run-animation");
        AnimatedSprite.LayerDepth = 0.5f;

        KinematicBase.Collider = new Collider(new Rectangle(400, 150, 16, 16), true, this);

        Health = 5;
    }

    public void Update(GameTime gameTime)
    {
        float forwardOffset = KinematicBase.Collider.Rect.Width / 2f + 5f;

        Vector2 rayStart = new Vector2(
            KinematicBase.Collider.Rect.Center.X + forwardOffset * Direction,
            KinematicBase.Collider.Rect.Bottom
        );

        EnemyRay = new Ray2D(rayStart, new Vector2(0, 1), 50);

        RayColliding = EnemyRay.Raycast(Collider.RectangleList);

        HandleDamage();

        HandleGravity();

        HandleMovement();

        FlipSprite(Direction);

        if (Health <= 0)
        {
            Free();
        }

        KinematicBase.UpdateCollider(gameTime);

        if (KinematicBase.IsOnGround())
        {
            KinematicBase.Velocity.Y = 0;
        }

        SpritePosition = new Vector2(KinematicBase.Collider.Rect.X, KinematicBase.Collider.Rect.Y);

        AnimatedSprite.PlayAnimation(RunAnimation, true);

        AnimatedSprite.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        DrawSprites(spriteBatch, SpritePosition, TextureOffset);
        //DrawHelper.DrawRay(EnemyRay, Color.Blue, 2f);
        //DrawHelper.DrawRectangle(KinematicBase.Collider.Rect, Color.Red, 2);
    }

    private void FlipSprite(int direction)
    {
        if (direction == 1)
        {
            if (AnimatedSprite != null) AnimatedSprite.Effects = SpriteEffects.None;
            if (AnimatedSpriteFeet != null) AnimatedSpriteFeet.Effects = SpriteEffects.None;
        }
        else
        {
            if (AnimatedSprite != null) AnimatedSprite.Effects = SpriteEffects.FlipHorizontally;
            if (AnimatedSpriteFeet != null) AnimatedSpriteFeet.Effects = SpriteEffects.FlipHorizontally;
        }
    }
        
    private void HandleGravity()
    {
        KinematicBase.Velocity.Y += Gravity * Core.DeltaTime;
    }

    private void HandleMovement()
    {
        
        if (!RayColliding)
        {
            Direction = -Direction;
        }

        KinematicBase.Velocity.X = 80 * Direction;
    }

    private void HandleDamage()
    {
        for (int i = 0; i < Collider.ColliderList.Count; i++)
        {
            Collider collider = Collider.ColliderList[i];

            if (collider.Circ != null)
            {
                if (collider.Enabled)
                {
                    if (CanTakeDamage)
                    {
                        TakeDamage(1);
                    }
                }
            }
        }



    }
    

    public void TakeDamage(int DamageAmount)
    {
        CanTakeDamage = false;
        Health -= DamageAmount;

        Console.WriteLine(Health);
        Timer.Wait(0.7f, () => { CanTakeDamage = true; });
    }

}
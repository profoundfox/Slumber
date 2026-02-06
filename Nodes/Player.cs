namespace Slumber
{
    public class Player : KinematicBody2D
    {
        public float MoveSpeed = 1000f;
        public float Acceleration = 3500f;
        public float Deceleration = 2500f;
        public float Gravity = 1300f;
        public float TerminalVelocity = 1200f;
        public float JumpForce = -350f;

        public float WallSlideGravity = 200f;
        public float WallJumpHorizontalSpeed = 200f;
        public float WallJumpVerticalSpeed = 300f;

        public int PlayerAxis;

        public AnimatedSprite2D Sprite;

        private bool jumpReleased = false;

        public Player(KinematicBodyConfig cfg) : base(cfg) {}

        public override void Load()
        {
            base.Load();

            var c = new CollisionShape2D(new CollisionShapeConfig
            {
                Parent = this,
                Shape = new RectangleShape2D(10, 25)
            });

            new StaticBody2D(new StaticBodyConfig
            {
                CollisionShape = c
            });

            var animations = AsepriteLoader.LoadAnimations(
                new("PlayerModel3Atlas"),
                PathHelper.Combine("Raw/PlayerModel3.json")
            );

            Sprite = new AnimatedSprite2D(new AnimatedSpriteConfig
            {
                Parent = this,
                Name = "PlayerSprite",
                Atlas = animations,
                LocalPosition = new Vector2(2, 9),
                IsLooping = true
            });
        }

        public override void PhysicsUpdate(float delta)
        {
            PlayerAxis = Engine.Input.GetAxis("MoveLeft", "MoveRight");

            HandleJump();
            HandleMovementInput();
            HandleDeceleration(delta);
            ApplyGravity(delta);

            base.PhysicsUpdate(delta);
        }

        public override void ProcessUpdate(float delta)
        {
            base.ProcessUpdate(delta);

            AnimateSprite();
            FlipSprite();
        }

        private void ApplyGravity(float delta)
        {
            if (!IsOnFloor)
            {
                Velocity = new Vector2(Velocity.X, Velocity.Y + Gravity * delta);
            }

            else if (Velocity.Y > 0)
            {
                Velocity.Y = 0;
            }
        }

        public void ApplyGravity()
        {
            if (!IsOnFloor)
            {
                Velocity.Y = MathF.Min(
                    Velocity.Y + Gravity * Engine.DeltaTime,
                    TerminalVelocity
                );
            }
            else if (Velocity.Y > 0)
            {
                Velocity.Y = 0;
            }
        }


        public void HandleMovementInput()
        {
            
            float targetSpeed = MoveSpeed * PlayerAxis;

            if (targetSpeed != 0)
            {
                Velocity.X = MoveToward(Velocity.X, targetSpeed, Acceleration);
            }
        }

        public void HandleDeceleration(float delta)
        {
            if (!Engine.Input.IsActionPressed("MoveLeft") && !Engine.Input.IsActionPressed("MoveRight"))
            {
                Velocity.X = MoveToward(Velocity.X, 0, Deceleration * delta);
            }
        }

        public void HandleJump()
        {
            if (IsOnFloor)
            {
                if (Engine.Input.IsActionJustPressed("Jump"))
                {
                    Velocity.Y = JumpForce;
                    jumpReleased = false;
                }

                if (!jumpReleased && Engine.Input.IsActionJustReleased("Jump") && Velocity.Y < 0)
                {
                    Velocity.Y /= 4f;
                    jumpReleased = true;
                }
            }
        }

        public float MoveToward(float current, float target, float maxDelta)
        {
            if (MathF.Abs(target - current) <= maxDelta) return target;
            return current + MathF.Sign(target - current) * maxDelta;
        }

        private void AnimateSprite()
        {
            if (IsOnFloor)
            {
                if (PlayerAxis != 0)
                    Sprite.PlayAnimation("Run");
                else
                    Sprite.PlayAnimation("Idle");
            }
            else
            {
                Sprite.PlayAnimation("Fall");
            }
        }

        private void FlipSprite()
        {
            if (PlayerAxis > 0)
                Sprite.LocalMaterial = Sprite.LocalMaterial with { SpriteEffects = SpriteEffects.None };
            else if (PlayerAxis < 0)
                Sprite.LocalMaterial = Sprite.LocalMaterial with { SpriteEffects = SpriteEffects.FlipHorizontally };
        }
    }
}
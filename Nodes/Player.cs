namespace Slumber
{
    public class Player : KinematicBody2D
    {
        public float MoveSpeed = 100f;
        public float Acceleration = 3500f;
        public float Deceleration = 2500f;
        public float Gravity = 1300f;
        public float TerminalVelocity = 1200f;
        public float JumpForce = -350f;

        public float WallSlideGravity = 10f;
        public float WallJumpHorizontalSpeed = 200f;
        public float WallJumpVerticalSpeed = 300f;

        public bool AllowControl = true;

        public float CoyoteTime = 0.2f;
        public float BufferTime = 0.2f;

        public int PlayerAxis;
        public int PlayerDirection;

        public AnimatedSprite2D Sprite;

        private bool jumpReleased = false;
        private bool wallSlideTriggered = false;
        private bool jumpBuffered = false;
        private bool canCoyoteJump = false;
        private bool wasOnFloor = false;


        public Player(KinematicBodyConfig cfg) : base(cfg) {}

        public override void Load()
        {
            base.Load();

            CollisionShape = new CollisionShape2D(new CollisionShapeConfig
            {
                Parent = this,
                Shape = new RectangleShape2D(10, 25)
            });

            var animations = AsepriteLoader.LoadAnimations(
                new("Assets/Animations/PlayerModel3Atlas"),
                PathHelper.Combine("Raw/Raw/PlayerModel3.json")
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

            PlayerDirection = PlayerAxis != 0 ? PlayerAxis : PlayerDirection;

            HandleCoyoteTime();
            HandleJump();
            HandleMovementInput();
            HandleWallSlide();
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

        public void HandleWallSlide()
        {
            if ((PlayerAxis > 0 || PlayerAxis < 0) && IsOnWall && !IsOnFloor)
                wallSlideTriggered = true; 

            if (!wallSlideTriggered)
                return;
            
            if (!IsOnWall || IsOnFloor)
                wallSlideTriggered = false;

            Velocity.Y = MathF.Min(
                Velocity.Y + WallSlideGravity,
                WallSlideGravity);
            
            if (Engine.Input.IsActionJustPressed("Jump"))
            {
                WallJump();
            }
        }

        public void WallJump()
        {
            AllowControl = false;

            Engine.Timer.Wait(0.06f, () => AllowControl = true);

            if (PlayerDirection == 1)
                Velocity.X = -WallJumpHorizontalSpeed;
            
            if (PlayerDirection == -1)
                Velocity.X = WallJumpHorizontalSpeed;

            Velocity.Y = -WallJumpVerticalSpeed;
        }
        public void ApplyGravity(float delta)
        {
            if (wallSlideTriggered)
                return;

            if (!IsOnFloor)
            {
                Velocity.Y = MathF.Min(
                    Velocity.Y + Gravity * delta,
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
            if (!AllowControl)
                return;
            
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

        private void HandleCoyoteTime()
        {
            if (wasOnFloor && !IsOnFloor)
            {
                canCoyoteJump = true;
                Engine.Timer.Wait(CoyoteTime, () => canCoyoteJump = false);
            }

            if (IsOnFloor)
            {
                canCoyoteJump = false;
            }

            wasOnFloor = IsOnFloor;
        }


        public void HandleJump()
        {
            if (IsOnFloor || canCoyoteJump)
            {
                if (Engine.Input.IsActionJustPressed("Jump") || jumpBuffered)
                {
                    Velocity.Y = JumpForce;
                    jumpReleased = false;
                    canCoyoteJump = false;
                    jumpBuffered = false;
                }

                if (!jumpReleased && Engine.Input.IsActionJustReleased("Jump") && Velocity.Y < 0)
                {
                    Velocity.Y /= 4f;
                    jumpReleased = true;
                }
            }
            else
            {
                if (Engine.Input.IsActionJustPressed("Jump"))
                {
                    jumpBuffered = true;
                    Engine.Timer.Wait(BufferTime, () => jumpBuffered = false);
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
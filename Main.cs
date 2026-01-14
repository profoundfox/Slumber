using System.Collections.Generic;

namespace Slumber
{
    public class Main : Engine
    {
        public Main() : base(new EngineConfig
        {
            Resources = new ContentPipelineLoader(),
            Title = "Slumber",
            DebugMode = true,
            Maximised = false,
            IsBorderless = false,
            ExitOnEscape = true,
        }) {}

        protected override void Initialize()
        {
            base.Initialize();

            Stage.AddStage(new Scene1());

            DebugOverlay.AddInfo("PlayerLocation", () =>
            {
                var p = Node.GetFirstNodeByT<Player>();
                return p == null ? "Player: Null" : $"Player Position: {p.GlobalTransform.Position}";
            }, Color.Yellow);
            
            DebugOverlay.AddInfo("PlayerState", () =>
            {
                var p = Node.GetFirstNodeByT<Player>();                
                return p == null ? "Player: Null" : $"Player State: {p.StateController.CurrentState}";
            }, Color.Yellow);
            
            Input.AddBind("MoveLeft", new InputAction(Keys.A), new InputAction(Buttons.DPadLeft));
            Input.AddBind("MoveRight", new InputAction(Keys.D), new InputAction(Buttons.DPadRight));
            Input.AddBind("MoveDown", new InputAction(Keys.W), new InputAction(Buttons.DPadDown));
            Input.AddBind("MoveUp", new InputAction(Keys.S), new InputAction(Buttons.DPadUp));

            Input.AddBind("Jump", new InputAction(Keys.Space), new InputAction(Buttons.A));

            Input.AddBind("Attack", new InputAction(MouseButton.Left), new InputAction(Buttons.Y));

            Input.AddBind("Pause", new InputAction(Keys.Escape), new InputAction(Buttons.Start));
            Input.AddBind("Back", new InputAction(Keys.X), new InputAction(Buttons.B));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}


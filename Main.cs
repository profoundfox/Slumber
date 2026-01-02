using System.Collections.Generic;

namespace Slumber
{
    public class Main : Engine
    {
        public Main() : base(new EngineConfig
        {
            Resources = new ContentPipelineLoader(),
            Title = "Slumber",
            FontPath = "Assets/Fonts/Font",
            DebugMode = true,
            Maximised = true,
            IsBorderless = false,
            ExitOnEscape = true,
            Actions = 
            {
                {"MoveLeft", new List<InputAction> { new InputAction(Keys.Left), new InputAction(Buttons.DPadLeft) }},
                {"MoveRight", new List<InputAction> { new InputAction(Keys.Right), new InputAction(Buttons.DPadRight) }},
                {"Jump", new List<InputAction> { new InputAction(Keys.Z), new InputAction(Buttons.A) }},
                {"Attack", new List<InputAction> { new InputAction(Keys.X), new InputAction(Buttons.Y), new InputAction(MouseButton.Left) }},
                {"Pause", new List<InputAction> { new InputAction(Keys.Escape), new InputAction(Buttons.Start) }},
                {"Back", new List<InputAction> { new InputAction(Keys.X), new InputAction(Buttons.B) }}
            }
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

            Input.AddBind("MoveDown", [new InputAction(Keys.S), new InputAction(Keys.Down)]);
            Input.AddBind("MoveUp", [new InputAction(Keys.W), new InputAction(Keys.Up)]);
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


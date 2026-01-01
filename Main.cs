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
            Maximised = false,
            IsBorderless = false,
            ExitOnEscape = true
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


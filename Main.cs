namespace Slumber
{
    public class Main : Engine
    {
        public Main() : base(new EngineConfig
        {
            ContentProvider = new ContentPipelineLoader(),
            Title = "Slumber",
            FontPath = "Assets/Fonts/Font",
            GumProject = "GumProject/GumProject.gumx",
            MainCharacterType = typeof(Player),
            DebugMode = true,
            Maximised = false,
            IsBorderless = false
        }) {}

        protected override void Initialize()
        {
            base.Initialize();

            SceneManager.AddScene(new MainMenu());

            DebugOverlay.AddInfo("PlayerLocation", () =>
            {
                var p = NodeManager.GetNodeByType<Player>();
                
                return p == null ? "Player: Null" : $"Player Position: {p.GlobalTransform.Position}";
            }, Color.Yellow);
           
            DebugOverlay.AddInfo("PlayerState", () =>
            {
                var p = NodeManager.GetNodeByType<Player>();                
                return p == null ? "Player: Null" : $"Player State: {p.StateController.CurrentState}";
            }, Color.Yellow);
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


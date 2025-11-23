
namespace Slumber.Screens
{
    partial class PauseMenu
    {
        partial void CustomInitialize()
        {
            Root.Visible = false;

            ResumeButton.IsFocused = true;
            
            ResumeButton.Click += (_, _) =>
            {
                Engine.SceneManager.UnfreezeCurrentScene();
                Root.Visible = false;
            };

            QuitButton.Click += (_, _) =>
            {
                SaveManager.SaveData();
                Engine.SceneManager.AddScene(new MainMenu());
            };

            SettingsButton.Click += (_, _) =>
            {
                Settings.IsVisible = true;
                Main.Visible = false;
                Settings.ControlButton.IsFocused = true;
            };


            

        
        }
    }
}

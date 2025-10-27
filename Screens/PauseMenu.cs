using ConstructEngine;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace Slumber.Screens
{
    partial class PauseMenu
    {
        partial void CustomInitialize()
        {
            Root.Visible = false;

            ResumeButton.IsFocused = true;

            Settings.Visible = false;
            
            ResumeButton.Click += (_, _) =>
            {
                Core.SceneManager.SceneFrozen = false;
                Root.Visible = false;
            };

            QuitButton.Click += (_, _) =>
            {
                Core.SceneManager.AddScene(new MainMenu());
            };

            SettingsButton.Click += (_, _) =>
            {
                Settings.Visible = true;
                Main.Visible = false;
            };

            Back.Click += (_, _) =>
            {
                Settings.Visible = false;
                Main.Visible = true;
            };
        }
    }
}

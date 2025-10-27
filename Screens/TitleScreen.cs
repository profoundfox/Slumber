using System;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;
using ConstructEngine;
using Microsoft.Xna.Framework;

namespace Slumber.Screens
{
    partial class TitleScreen
    {
        partial void CustomInitialize()
        {
            StartButton.IsFocused = true;
            Main.Visible = true;
            Settings.Visible = false;
            
            StartButton.Click += (_, _) =>
            {
                Core.SceneManager.AddScene(new Scene1());
            };

            QuitButton.Click += (_, _) =>
            {
                Core.Exit = true;
            };

            SettingsButton.Click += (_, _) =>
            {
                Settings.Visible = true;
                Main.Visible = false;
                MasterSlider.IsFocused = true;
            };

            Back.Click += (_, _) =>
            {
                Settings.Visible = false;
                Main.Visible = true;
                StartButton.IsFocused = true;
            };

            MusicSlider.Value = 10;
        

            

        }
    }
}

using System;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;
using ConstructEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Slumber.Screens
{
    partial class TitleScreen
    {
        partial void CustomInitialize()
        {   
            StartButton.IsFocused = true;
            Main.Visible = true;
            Settings.IsVisible = false;

            
            StartButton.Click += (_, _) =>
            {
                Core.SceneManager.AddScene(new Scene1());
            };

            ResumeButton.Click += (_, _) =>
            {
                SaveManager.LoadData();
            };


            QuitButton.Click += (_, _) =>
            {
                Core.Exit = true;
            };

            SettingsButton.Click += (_, _) =>
            {
                Settings.IsVisible = true;
                Main.Visible = false;
                Settings.ControlButton.IsFocused = true;
            };

            Settings.ControlButton.Click += (_, _) =>
            {
                Settings.IsVisible = false;
                Controls.IsVisible = true;
            };

            Settings.MasterSlider.ValueChanged += (_, _) =>
            {
                Console.WriteLine(Settings.MasterSlider.Value);
            };

            Console.WriteLine(Settings.MasterSlider.Value);

            
        

            

        }
    }
}

using System;
using ConstructEngine;
using ConstructEngine.Components;
using ConstructEngine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Slumber.Components.ConstructControls;
using Slumber.Screens;

namespace Slumber.Logic;

public class Pausemenu : BackseatComponent
{
    private PauseMenu Menu;

    public Pausemenu(object root) : base(root)
    {
        Menu = new PauseMenu();
        GumHelper.AddScreenToRoot(Menu);
    }
    
    public override void Update(GameTime gameTime)
    {
        
        if (Core.Input.IsActionJustPressed("Pause"))
        {
            Core.SceneManager.QueeFreezeCurrentScene();
            Menu.Root.Visible = true;
        }

        if (Core.Input.IsActionJustPressed("Back"))
        {
            if (Menu.Main.Visible)
            {
                Core.SceneManager.UnFreezeCurrentScene();
                Menu.Root.Visible = false;
            }
            if (Menu.Settings.IsVisible)
            {
                Menu.Settings.IsVisible = false;
                Menu.Main.Visible = true;
                Menu.ResumeButton.IsFocused = true;
            }
            if (Menu.Settings.Controls.IsVisible)
            {
                Menu.Settings.Controls.IsVisible = false;
                Menu.Settings.IsVisible = true;
                Menu.Settings.ControlButton.IsFocused = true;
            }
        }

        
    }
}

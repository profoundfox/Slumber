using ConstructEngine;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Slumber.Components.ConstructControls
{
    partial class ControlComponent
    {
        private bool isRebinding;
        private ConstructButtonDuo currentRebindButton = null;
        private string currentAction;

        partial void CustomInitialize()
        {
            foreach (var kvp in Core.Input.Binds)
            {
                var keybindButton = new ConstructButtonDuo();
                KeybindButtons.AddChild(keybindButton);

                keybindButton.Width = 400;
                keybindButton.X = 125;
                keybindButton.TextLeft = kvp.Key;
                keybindButton.TextRight = kvp.Value.FirstOrDefault().Key.ToString();

                keybindButton.Click += (_, _) =>
                {
                    if (!isRebinding)
                    {
                        currentRebindButton = keybindButton;
                        currentAction = kvp.Key;
                        keybindButton.TextRight = "Press a key...";
                    }
                };
            }
        }

        public void Update()
        {
            if (isRebinding)
            {
                Keys remapKey = Core.I
            }
        }
    }
}

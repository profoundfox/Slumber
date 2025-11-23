
namespace Slumber.Components.ConstructControls
{
    partial class Settings : IGumUpdatable
    {
        partial void CustomInitialize()
        {
            GumManager.Register(this);
            
            ControlButton.Click += (_, _) =>
            {
                Main.IsVisible = false;
                Controls.IsVisible = true;
                Controls.ResetBindsButton.IsFocused = true;
            };
        }

        public void Update(GameTime gameTime)
        {
            Controls.Update(Engine.DeltaTime);
        }

        


    }
}

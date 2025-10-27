//Code for PauseMenu
using GumRuntime;
using System.Linq;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Slumber.Components.Controls;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace Slumber.Screens;
partial class PauseMenu : MonoGameGum.Forms.Controls.FrameworkElement
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new global::Gum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new global::MonoGameGum.GueDeriving.ContainerRuntime();
            var element = ObjectFinder.Self.GetElementSave("PauseMenu");
#if DEBUG
if(element == null) throw new System.InvalidOperationException("Could not find an element named PauseMenu - did you forget to load a Gum project?");
#endif
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new PauseMenu(visual);
            visual.Width = 0;
            visual.WidthUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            visual.Height = 0;
            visual.HeightUnits = global::Gum.DataTypes.DimensionUnitType.RelativeToParent;
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(PauseMenu)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("PauseMenu", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }
    public ColoredRectangleRuntime ColoredRectangleInstance { get; protected set; }
    public ContainerRuntime Main { get; protected set; }
    public ButtonStandard ResumeButton { get; protected set; }
    public ButtonStandard SettingsButton { get; protected set; }
    public ButtonStandard QuitButton { get; protected set; }
    public ContainerRuntime Root { get; protected set; }
    public ContainerRuntime Settings { get; protected set; }
    public ButtonStandard Volume { get; protected set; }
    public ButtonStandard Input { get; protected set; }
    public ButtonStandard SFX { get; protected set; }
    public ButtonStandard Back { get; protected set; }

    public PauseMenu(InteractiveGue visual) : base(visual)
    {
    }
    public PauseMenu()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        ColoredRectangleInstance = this.Visual?.GetGraphicalUiElementByName("ColoredRectangleInstance") as global::MonoGameGum.GueDeriving.ColoredRectangleRuntime;
        Main = this.Visual?.GetGraphicalUiElementByName("Main") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        ResumeButton = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"ResumeButton");
        SettingsButton = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"SettingsButton");
        QuitButton = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"QuitButton");
        Root = this.Visual?.GetGraphicalUiElementByName("Root") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        Settings = this.Visual?.GetGraphicalUiElementByName("Settings") as global::MonoGameGum.GueDeriving.ContainerRuntime;
        Volume = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"Volume");
        Input = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"Input");
        SFX = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"SFX");
        Back = global::Gum.Forms.GraphicalUiElementFormsExtensions.TryGetFrameworkElementByName<ButtonStandard>(this.Visual,"Back");
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}

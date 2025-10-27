//Code for ConstructControls/ConstructLabel (Text)
using GumRuntime;
using System.Linq;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using Gum.Converters;
using Gum.DataTypes;
using Gum.Managers;
using Gum.Wireframe;

using RenderingLibrary.Graphics;

using System.Linq;

namespace Slumber.Components.ConstructControls;
partial class ConstructLabel : global::Gum.Forms.Controls.Label
{
    [System.Runtime.CompilerServices.ModuleInitializer]
    public static void RegisterRuntimeType()
    {
        var template = new global::Gum.Forms.VisualTemplate((vm, createForms) =>
        {
            var visual = new global::MonoGameGum.GueDeriving.TextRuntime();
            var element = ObjectFinder.Self.GetElementSave("ConstructControls/ConstructLabel");
#if DEBUG
if(element == null) throw new System.InvalidOperationException("Could not find an element named ConstructControls/ConstructLabel - did you forget to load a Gum project?");
#endif
            element.SetGraphicalUiElement(visual, RenderingLibrary.SystemManagers.Default);
            if(createForms) visual.FormsControlAsObject = new ConstructLabel(visual);
            return visual;
        });
        global::Gum.Forms.Controls.FrameworkElement.DefaultFormsTemplates[typeof(ConstructLabel)] = template;
        ElementSaveExtensions.RegisterGueInstantiation("ConstructControls/ConstructLabel", () => 
        {
            var gue = template.CreateContent(null, true) as InteractiveGue;
            return gue;
        });
    }

    public ConstructLabel(InteractiveGue visual) : base(visual)
    {
    }
    public ConstructLabel()
    {



    }
    protected override void ReactToVisualChanged()
    {
        base.ReactToVisualChanged();
        CustomInitialize();
    }
    //Not assigning variables because Object Instantiation Type is set to By Name rather than Fully In Code
    partial void CustomInitialize();
}

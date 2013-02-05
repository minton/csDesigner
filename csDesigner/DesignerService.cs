using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace csDesigner
{
    internal class DesignerService : DesignerOptionService
    {
        protected override void PopulateOptionCollection(DesignerOptionCollection options)
        {
            if (options.Parent != null) return;

            var designerOptions = new DesignerOptions {UseSnapLines = true, UseSmartTags = true};
            var formsDesigner = CreateOptionCollection(options, "WindowsFormsDesigner", null);
            CreateOptionCollection(formsDesigner, "General", designerOptions);
        }
    }
}
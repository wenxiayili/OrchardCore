using System.Threading.Tasks;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Forms.Models;
using OrchardCore.Forms.ViewModels;

namespace OrchardCore.Forms.Drivers
{
    public class TextAreaPartDisplay : ContentPartDisplayDriver<TextAreaPart>
    {
        public override IDisplayResult Display(TextAreaPart part)
        {
            return View("TextAreaPart", part).Location("Detail", "Content");
        }

        public override IDisplayResult Edit(TextAreaPart part, BuildPartEditorContext context)
        {
            return Initialize<TextAreaPartEditViewModel>("TextAreaPart_Edit", m =>
            {
                m.Name = part.Name;
                m.Label = part.Label;
                m.Placeholder = part.Placeholder;
                m.DefaultValue = part.DefaultValue;
            });
        }

        public async override Task<IDisplayResult> UpdateAsync(TextAreaPart part, IUpdateModel updater)
        {
            var viewModel = new InputPartEditViewModel();

            if (await updater.TryUpdateModelAsync(viewModel, Prefix))
            {
                part.Name = viewModel.Name?.Trim();
                part.Label = viewModel.Label?.Trim();
                part.Placeholder = viewModel.Placeholder?.Trim();
                part.DefaultValue = viewModel.DefaultValue?.Trim();
            }

            return Edit(part);
        }
    }
}

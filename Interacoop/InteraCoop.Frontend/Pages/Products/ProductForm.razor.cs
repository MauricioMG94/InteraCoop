using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Helpers;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;

namespace InteraCoop.Frontend.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public partial class ProductForm
    {
        private EditContext editContext = null!;

        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter, EditorRequired] public ProductDto Product { get; set; } = null!;
        [Parameter, EditorRequired] public EventCallback OnValidSubmit { get; set; }
        [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }
        [Parameter] public bool IsEdit { get; set; } = false;
        [Parameter] public EventCallback AddImageAction { get; set; }
        [Parameter] public EventCallback RemoveImageAction { get; set; }

        public bool FormPostedSuccessfully { get; set; } = false;

        protected override void OnInitialized()
        {
            editContext = new(Product);
        }
        private async Task OnBeforeInternalNavigation(LocationChangingContext context)
        {
            var formWasEdited = editContext.IsModified();

            if (!formWasEdited)
            {
                return;
            }

            if (FormPostedSuccessfully)
            {
                return;
            }

            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = "¿Deseas abandonar la página y perder los cambios?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true
            });

            var confirm = !string.IsNullOrEmpty(result.Value);

            if (confirm)
            {
                return;
            }

            context.PreventNavigation();
        }
    }
}
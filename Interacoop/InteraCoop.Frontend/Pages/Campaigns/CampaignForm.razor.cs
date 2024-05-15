using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Helpers;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;

namespace InteraCoop.Frontend.Pages.Campaigns
{
    [Authorize(Roles = "Analist")]
    public partial class CampaignForm
    {

        private EditContext editContext = null!;

        private List<MultipleSelectorModel> selected { get; set; } = new();
        private List<MultipleSelectorModel> nonSelected { get; set; } = new();

        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Parameter, EditorRequired] public CampaignDto Campaign { get; set; } = null!;
        [Parameter, EditorRequired] public EventCallback OnValidSubmit { get; set; }
        [Parameter, EditorRequired] public EventCallback ReturnAction { get; set; }
        [Parameter, EditorRequired] public List<Product> nonSelectedCampaigns { get; set; } = new();
        [Parameter] public List<Product> selectedCampaigns { get; set; } = new();
        public bool FormPostedSuccessfully { get; set; } = false;

        protected override void OnInitialized()
        {
            editContext = new(Campaign);

            Campaign.StartDate = DateTime.Today;
            Campaign.EndDate = DateTime.Today;

            selected = selectedCampaigns.Select(x => new MultipleSelectorModel(x.Id.ToString(), x.Name)).ToList();
            nonSelected = nonSelectedCampaigns.Select(x => new MultipleSelectorModel(x.Id.ToString(), x.Name)).ToList();
        }

        private async Task OnDataAnnotationsValidatedAsync()
        {
            Campaign.ProductsIds = selected.Select(x => int.Parse(x.Key)).ToList();
            await OnValidSubmit.InvokeAsync();
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
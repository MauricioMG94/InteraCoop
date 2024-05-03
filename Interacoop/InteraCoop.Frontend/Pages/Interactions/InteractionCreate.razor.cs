using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Pages.Opportunities;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Components;


namespace InteraCoop.Frontend.Pages.Interactions
{
    public partial class InteractionCreate
    {
        private InteractionDto interaction = new()
        {
            ClientsIds = new List<int>()
        };

        private InteractionForm? interactionForm;
        private List<Client> selectedInteractions = new();
        private List<Client> nonSelectedInteractions = new();
        private bool loading = true;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            var httpActionResponse = await Repository.GetAsync<List<Client>>("/api/clients");
            loading = false;

            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            nonSelectedInteractions = httpActionResponse.Response!;
        }

        private async Task CreateAsync()
        {
            var httpActionResponse = await Repository.PostAsync("api/interactions/new", interaction);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            Return();
        }

        private void Return()
        {
            interactionForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/interactions");
        }

    }
}

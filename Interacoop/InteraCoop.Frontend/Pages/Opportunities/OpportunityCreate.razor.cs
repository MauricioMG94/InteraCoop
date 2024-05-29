using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Opportunities
{
    [Authorize(Roles = "Employee")]
    public partial class OpportunityCreate
    {
        private OpportunityDto opportunity = new() {};

        private OpportunityForm? opportunityForm;

        private bool loading = true;
        public List<Campaign> Campaigns { get; set; } = new();
        public Interaction? Interaction { get; set; }
        public string? formName { get; set; }
        [Parameter] public int InteractionId { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;



        protected override async Task OnInitializedAsync()
        {
            await getCampaigns();
            await getInteraction();
        }

        private async Task getCampaigns()
        {
            var httpActionResponse = await Repository.GetAsync<List<Campaign>>("/api/campaigns");
            loading = false;

            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            Campaigns = httpActionResponse.Response!;
        }

        private async Task getInteraction()
        {
            var httpActionResponse = await Repository.GetAsync<Interaction>($"/api/interactions/{InteractionId}");
            loading = false;

            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            Interaction = httpActionResponse.Response!;
            formName = $"Crear oportunidad para la interacción del: {Interaction.StartDate.ToString("dd/MM/yyyy")}, con el cliente: {Interaction.Client.Name}.";
        }

        private async Task CreateAsync()
        {
            opportunity.InteractionId = InteractionId; 
            var httpActionResponse = await Repository.PostAsync("api/opportunities/new", opportunity);
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
            opportunityForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/opportunities");
        }
    }
}

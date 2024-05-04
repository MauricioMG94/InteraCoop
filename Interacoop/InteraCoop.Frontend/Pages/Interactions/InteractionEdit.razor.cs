using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Pages.Opportunities;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Interactions
{
    public partial class InteractionEdit
    {
        private InteractionDto interactionDto = new()
        {
            ClientsIds = new List<int>(),
        };

        private InteractionForm? interactionForm;
        private List<Client> selectedInteractions = new();
        private List<Client> nonSelectedInteractions = new();
        private bool loading = true;
        private Interaction? interaction;
        [Parameter] public int InteractionId { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadInteractionAsync();
            await LoadClientsAsync();
        }

        private async Task LoadInteractionAsync()
        {
            loading = true;
            var httpActionResponse = await Repository.GetAsync<Interaction>($"/api/interactions/{InteractionId}");

            if (httpActionResponse.Error)
            {
                loading = false;
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            interaction = httpActionResponse.Response!;
            interactionDto = ToInteractionDto(interaction);
            loading = false;
        }

        private InteractionDto ToInteractionDto(Interaction interaction)
        {
            return new InteractionDto
            {
                Id = interaction.Id,
                InteractionType = interaction.InteractionType,
                InteractionCreationDate = interaction.InteractionCreationDate,
                StartDate = interaction.StartDate,
                EndDate = interaction.EndDate,
                Address = interaction.Address,
                ObservationsInteraction = interaction.ObservationsInteraction,
                Office = interaction.Office,
                AuditDate = interaction.AuditDate,
                AuditUser = interaction.AuditUser,
                ClientsIds = interaction.ClientsList!.Select(x => x.Id).ToList()
            };
        }

        private async Task LoadClientsAsync()
        {
            loading = true;
            var httpActionResponse = await Repository.GetAsync<List<Client>>("/api/clients");

            if (httpActionResponse.Error)
            {
                loading = false;
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            var clients = httpActionResponse.Response!;
            foreach (var client in clients!)
            {
                var found = interaction?.ClientsList?.FirstOrDefault(x => x.Id == client.Id);
                if (found == null)
                {
                    nonSelectedInteractions.Add(client);
                }
                else
                {
                    selectedInteractions.Add(client);
                }
            }
            loading = false;
        }

       
        private async Task SaveChangesAsync()
        {
            var httpActionResponse = await Repository.PutAsync("/api/interactions/update", interactionDto);
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

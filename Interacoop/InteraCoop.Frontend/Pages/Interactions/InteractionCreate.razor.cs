using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;
using System.Text.Json;

namespace InteraCoop.Frontend.Pages.Interactions
{
    [Authorize(Roles = "Employee")]
    public partial class InteractionCreate
    {
        private InteractionDto interaction = new() {};

        private InteractionForm? interactionForm;

        private bool loading = true;
        public List<Client> Clients{ get; set; } = new();
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

            Clients = httpActionResponse.Response!;
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

            var data = httpActionResponse.HttpResponseMessage.Content.ReadAsStringAsync().Result;
            using (JsonDocument doc = JsonDocument.Parse(data))
            {
                int newId = doc.RootElement.GetProperty("id").GetInt32();
                Return();
                RedirectToOpportunity(newId);
            }
            
        }

        private void Return()
        {
            interactionForm!.FormPostedSuccessfully = true;
        }

        private void RedirectToOpportunity(int objectId)
        {
            NavigationManager.NavigateTo($"/interaction/opportunity/create/{objectId}");
        }


    }
}

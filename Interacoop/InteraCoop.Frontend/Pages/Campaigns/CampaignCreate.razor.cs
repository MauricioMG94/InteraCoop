using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Campaigns
{
    [Authorize(Roles = "Analist")]
    public partial class CampaignCreate
    {        
        private CampaignDto campaign = new()
        {
            ProductsIds = new List<int>()
        };

        private CampaignForm? campaignForm;
        private List<Product> selectedCampaigns = new();
        private List<Product> nonSelectedCampaigns = new();
        private bool loading = true;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            var httpActionResponse = await Repository.GetAsync<List<Product>>("/api/products/all");
            loading = false;

            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            nonSelectedCampaigns = httpActionResponse.Response!;
        }

        private async Task CreateAsync()
        {
            var httpActionResponse = await Repository.PostAsync("/api/campaigns/new", campaign);
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
            campaignForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/campaigns");
        }

    }
}
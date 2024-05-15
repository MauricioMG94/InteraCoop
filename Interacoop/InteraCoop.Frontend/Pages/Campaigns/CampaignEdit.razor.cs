using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Pages.Products;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Campaigns
{
    [Authorize(Roles = "Analist")]
    public partial class CampaignEdit
    {
        private CampaignDto campaignDto = new()
        {
            ProductsIds = new List<int>(),
        };

        private CampaignForm? campaignForm;
        private List<Product> selectedCampaigns = new();
        private List<Product> nonSelectedCampaigns = new();
        private bool loading = true;
        private Campaign? campaign;
        [Parameter] public int CampaignId { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadCampaignAsync();
            await LoadProductsAsync();
        }

        private async Task LoadCampaignAsync()
        {
            loading = true;
            var httpActionResponse = await Repository.GetAsync<Campaign>($"/api/campaigns/{CampaignId}");

            if (httpActionResponse.Error)
            {
                loading = false;
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            campaign = httpActionResponse.Response!;
            campaignDto = ToCampaignDto(campaign);
            loading = false;
        }

        private CampaignDto ToCampaignDto(Campaign campaign)
        {
            return new CampaignDto
            {
                Id = campaign.Id,
                CampaignId = campaign.CampaignId,
                CampaignName = campaign.CampaignName,
                CampaignType = campaign.CampaignType,
                Description = campaign.Description!,
                Status = campaign.Status,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                ProductsIds = campaign.ProductsList!.Select(x => x.Id).ToList()
            };
        }

        private async Task LoadProductsAsync()
        {
            loading = true;
            var httpActionResponse = await Repository.GetAsync<List<Product>>("/api/products/all");

            if (httpActionResponse.Error)
            {
                loading = false;
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            var products = httpActionResponse.Response!;
            foreach (var product in products!)
            {
                var found = campaign?.ProductsList?.FirstOrDefault(x => x.Id == product.Id);
                if (found == null)
                {
                    nonSelectedCampaigns.Add(product);
                }
                else
                {
                    selectedCampaigns.Add(product);
                }
            }
            loading = false;
        }

        private async Task SaveChangesAsync()
        {
            var httpActionResponse = await Repository.PutAsync("/api/campaigns/update", campaignDto);
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
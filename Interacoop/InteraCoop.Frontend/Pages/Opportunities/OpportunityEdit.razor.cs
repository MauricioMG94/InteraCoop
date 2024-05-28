using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Opportunities
{
    [Authorize(Roles = "Employee")]
    public partial class OpportunityEdit
    {
        private OpportunityDto opportunityDto = new() {};

        private OpportunityForm? opportunityForm;

        private bool loading = true;

        private Opportunity? opportunity;
        [Parameter] public int OpportunityId { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadOpportunityAsync();
        }

        private async Task LoadOpportunityAsync()
        {
            loading = true;
            var httpActionResponse = await Repository.GetAsync<Opportunity>($"/api/opportunities/{OpportunityId}");

            if (httpActionResponse.Error)
            {
                loading = false;
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            opportunity = httpActionResponse.Response!;
            opportunityDto = ToOpportunityDto(opportunity);
            loading = false;
        }

        private OpportunityDto ToOpportunityDto(Opportunity opportunity)
        {
            return new OpportunityDto
            {
                Id = opportunity.Id,
                Status = opportunity.Status,
                OpportunityObservations = opportunity.OpportunityObservations,
                RecordDate = opportunity.RecordDate,
                EstimatedAcquisitionDate = opportunity.EstimatedAcquisitionDate,
                CampaignId = opportunity.CampaignId,
                InteractionId = opportunity.InteractionId,
            };
        }

        private async Task SaveChangesAsync()
        {
            var httpActionResponse = await Repository.PutAsync("/api/opportunities/update", opportunityDto);
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

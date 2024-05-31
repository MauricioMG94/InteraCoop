using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Widgets
{
    public partial class CampaignsChart
    {
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        public int Count { get; set; }
        public List<Opportunity>? Opportunities { get; set; }
        public List<Campaign>? Campaigns { get; set; } = new List<Campaign>();
        public Campaign? Campaign { get; set; }
        List<int> CampaignsIds = new List<int>();
        List<string> CampaignsNames = new List<string>();
        List<double> CampaignsCount = new List<double>();
        List<double> CampaignsPercent = new List<double>();
        private List<string> CombinedLabels { get; set; } = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            await LoadOpportunitiesListAsync();
        }

        private async Task<bool> LoadOpportunitiesListAsync()
        {
            var url = $"api/opportunities/full";
            var responseHttp = await Repository.GetAsync<List<Opportunity>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Opportunities = responseHttp.Response;
            await ProcessOpportunitiesAsync(Opportunities);
            return true;
        }

        private async Task ProcessOpportunitiesAsync(List<Opportunity> opportunities)
        {
            GetUniqueCampaignsIdsList(opportunities);
            await GetUniqueCampaignsListAsync();
            CountCampaignOccurrences(opportunities);
        }

        private void GetUniqueCampaignsIdsList(List<Opportunity>? opportunities)
        {
            if (opportunities != null)
            {
                foreach (var opportunity in opportunities)
                {
                    if (!CampaignsIds.Contains(opportunity.CampaignId))
                    {
                        CampaignsIds.Add(opportunity.CampaignId);
                    }
                }
            }
        }

        private async Task GetUniqueCampaignsListAsync()
        {
            foreach (var CampaignId in CampaignsIds)
            {
                var url = $"api/campaigns/{CampaignId}";
                var responseHttp = await Repository.GetAsync<Campaign>(url);
                if (!responseHttp.Error && responseHttp.Response != null)
                {
                    Campaign = responseHttp.Response;
                    Campaigns.Add(Campaign);
                }
            }

            foreach (var campaign in Campaigns)
            {
                if (campaign != null && !string.IsNullOrEmpty(campaign.CampaignName))
                {
                    CampaignsNames.Add(campaign.CampaignName);
                }
            }
        }

        private void CountCampaignOccurrences(List<Opportunity> opportunities)
        {
            CampaignsCount.Clear();
            if (opportunities != null)
            {
                foreach (var campaign in CampaignsIds)
                {
                    Count = opportunities.Count(o => o.CampaignId == campaign);
                    CampaignsCount.Add(Count);
                }
            }
            GetCampaignParticipation(CampaignsCount);
        }

        private void GetCampaignParticipation(List<double> campaignsCount)
        {
            CampaignsPercent.Clear();

            double totalCamps = campaignsCount.Sum(); 

            if (totalCamps == 0) return;

            foreach (var value in campaignsCount)
            {
                double CampPercent = Math.Round((value / totalCamps) * 100, 1);
                CampaignsPercent.Add(CampPercent);
            }

            CombinedLabels = CampaignsNames
                .Select((value, index) => $"{value} ({CampaignsPercent[index]:F1}%)")
                .ToList();
        }
    }
}

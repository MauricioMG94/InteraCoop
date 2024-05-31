using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Widgets
{
    public partial class OpportunitiesChart
    {
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        public int Count { get; set; }
        public List<Opportunity>? Opportunities { get; set; }
        List<string> OpportunityStatus = new List<string>();
        List<double> StatusCount = new List<double> { 0 };
        private List<string> CombinedLabels { get; set; } = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            await LoadListAsync();
        }

        private async Task<bool> LoadListAsync()
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
            ProcessOpportunities(Opportunities);
            return true;
        }

        private void ProcessOpportunities(List<Opportunity> opportunities)
        {
            GetUniqueStatusList(opportunities);
            CountStatusOccurrences(opportunities);
        }

        private List<string> GetUniqueStatusList(List<Opportunity>? opportunities)
        {
            if (opportunities != null)
            {
                foreach (var opportunity in opportunities)
                {
                    if (!OpportunityStatus.Contains(opportunity.Status))
                    {
                        OpportunityStatus.Add(opportunity.Status);
                    }
                }
            }
            return OpportunityStatus;
        }

        private void CountStatusOccurrences(List<Opportunity> opportunities)
        {
            StatusCount.Clear();
            if (opportunities != null)
            {
                foreach(var status in OpportunityStatus)
                {
                    Count = opportunities.Count(o => o.Status == status);
                    StatusCount.Add(Count);   
                }
            }
            CombinedLabels = OpportunityStatus
            .Select((status, index) => $"{status} ({StatusCount[index]})")
            .ToList();
        }
    }
}


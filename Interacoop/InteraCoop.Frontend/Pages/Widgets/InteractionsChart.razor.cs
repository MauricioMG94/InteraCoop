using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Widgets
{
    public partial class InteractionsChart
    {
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        public int Count { get; set; }
        public List<Interaction>? Interactions{ get; set; }
        List<string> InteractionTypes = new List<string>();
        List<double> TypesCount = new List<double> { 0 };
        private List<string> CombinedLabels { get; set; } = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            await LoadListAsync();
        }

        private async Task<bool> LoadListAsync()
        {
            var url = $"api/interactions/full";
            var responseHttp = await Repository.GetAsync<List<Interaction>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            Interactions = responseHttp.Response;
            ProcessOpportunities(Interactions);
            return true;
        }

        private void ProcessOpportunities(List<Interaction> interactions)
        {
            GetUniqueStatusList(interactions);
            CountStatusOccurrences(interactions);
        }

        private List<string> GetUniqueStatusList(List<Interaction>? interactions)
        {
            if (interactions != null)
            {
                foreach (var opportunity in interactions)
                {
                    if (!InteractionTypes.Contains(opportunity.InteractionType))
                    {
                        InteractionTypes.Add(opportunity.InteractionType);
                    }
                }
            }
            return InteractionTypes;
        }

        private void CountStatusOccurrences(List<Interaction> interactions)
        {
            TypesCount.Clear();
            if (interactions != null)
            {
                foreach (var type in InteractionTypes)
                {
                    Count = interactions.Count(i => i.InteractionType == type);
                    TypesCount.Add(Count);
                }
            }
            CombinedLabels = InteractionTypes
            .Select((type, index) => $"{type} ({TypesCount[index]})")
            .ToList();
        }
    }
}

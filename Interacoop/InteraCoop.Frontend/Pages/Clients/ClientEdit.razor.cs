using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Entities;
using InteraCoop.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Clients
{
    [Authorize(Roles = "Employee")]
    public partial class ClientEdit
    {
        private ClientDto clientDto;
        /*= new()
        {
            CitiesIds = new List<int>(),
            //ProductImages = new List<string>()
        };*/
        //private FormWithName<Client>? clientForm;
        private ClientForm? clientForm;
        private List<City> selectedCities = new();
        private List<City> nonSelectedCities = new();
        private bool loading = true;
        private Client? client;
        [Parameter] public int ClientId { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await LoadClientAsync();
            //await LoadCategoriesAsync();
        }
        private ClientDto toClientDTO(Client client)
        {
            return new ClientDto
            {
                Telephone = client.Telephone,
                Id = client.Id,
                Name = client.Name,
                Document = client.Document,
                DocumentType = DocumentType.CC.ToString(),
                Address = client.Address,
                AuditUpdate = client.AuditUpdate,
                AuditUser= "Admin"
                //ProductCategoryIds = client.ProductCategories!.Select(x => x.CategoryId).ToList()
            };
        }

        private async Task LoadClientAsync()
        {
            loading = true;
            var httpActionResponse = await Repository.GetAsync<Client>($"/api/clients/{ClientId}");

            if (httpActionResponse.Error)
            {
                loading = false;
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            client = httpActionResponse.Response!;
            clientDto = toClientDTO(client);
            loading = false;
        }

        private async Task SaveChangesAsync()
        {
            var httpActionResponse = await Repository.PutAsync("/api/clients/full", clientDto);
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
            clientForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/clients");
        }
    }
}

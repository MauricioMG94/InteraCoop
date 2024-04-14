using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Countries
{
    public partial class CountryCreate
    {
        private Country country = new();
        private CountryForm? countryForm;

        [Inject] public IRepository repository { get; set; } = null!;
        [Inject] public SweetAlertService sweetAlertService { get; set; } = null!;
        [Inject] public NavigationManager navigationManager { get; set; } = null!;

        private async Task CreateAsync()
        {
            var response = await repository.PostAsync("api/countries", country);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await sweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            Return();
            var toast = sweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con exito.");
        }

        private void Return()
        {
            countryForm!.FormPostedSuccessfully = true;
            navigationManager.NavigateTo("/countries");
        }
    }
}
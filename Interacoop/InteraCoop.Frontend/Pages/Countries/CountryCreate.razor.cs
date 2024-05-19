using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Countries
{
    [Authorize(Roles = "Admin")]
    public partial class CountryCreate
    {
        private Country country = new();
        private CountryForm? countryForm;

        [Inject] public IRepository Repository { get; set; } = null!;
        [Inject] public SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] public NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
        
        private async Task CreateAsync()
        {
            var response = await Repository.PostAsync("api/countries", country);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            
            await BlazoredModal.CloseAsync(ModalResult.Ok());
            Return();
            
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
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
            NavigationManager.NavigateTo("/countries");
        }
    }
}
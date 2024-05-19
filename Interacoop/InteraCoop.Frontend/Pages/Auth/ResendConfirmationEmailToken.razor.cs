using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Auth
{
    public partial class ResendConfirmationEmailToken
    {
        private EmailDTO emailDTO = new();
        private bool loading;
        private bool wasClose;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private async Task ResendConfirmationEmailTokenAsync()
        {
            loading = true;
            var responseHttp = await Repository.PostAsync("/api/accounts/ResedToken", emailDTO);
            loading = false;
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                loading = false;
                return;
            }

            await SweetAlertService.FireAsync("Confirmación", "Se te ha enviado un correo electrónico con las instrucciones para activar tu usuario.", SweetAlertIcon.Info);
            NavigationManager.NavigateTo("/");
        }
        private async Task CloseModalAsync()
        {
            wasClose = true;
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }
    }
}
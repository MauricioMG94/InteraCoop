using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Auth
{
    public partial class ChangePassword
    {
        private ChangePasswordDTO changePasswordDTO = new();
        private bool loading;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        private async Task ChangePasswordAsync()
        {
            loading = true;

            try
            {
                if (changePasswordDTO == null)
                {
                    throw new NullReferenceException("changePasswordDTO is null");
                }

                var responseHttp = await Repository.PostAsync("/api/accounts/changePassword", changePasswordDTO);

                if (responseHttp == null)
                {
                    throw new NullReferenceException("responseHttp is null");
                }

                if (responseHttp.Error)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    if (string.IsNullOrEmpty(message))
                    {
                        message = "Unknown error occurred.";
                    }
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                }
                else
                {
                    NavigationManager.NavigateTo("/EditUser");
                    var toast = SweetAlertService.Mixin(new SweetAlertOptions
                    {
                        Toast = true,
                        Position = SweetAlertPosition.BottomEnd,
                        ShowConfirmButton = true,
                        Timer = 3000
                    });
                    await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Contraseña cambiada con éxito.");
                }
            }
            catch (NullReferenceException ex)
            {
                // Log the detailed error
                Console.Error.WriteLine($"NullReferenceException: {ex.Message}");
                await SweetAlertService.FireAsync("Error", $"Unhandled null reference: {ex.Message}", SweetAlertIcon.Error);
            }
            catch (Exception ex)
            {
                // Log the unexpected error
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
                await SweetAlertService.FireAsync("Error", $"Unexpected error: {ex.Message}", SweetAlertIcon.Error);
            }
            finally
            {
                loading = false;
            }
        }
    }
}

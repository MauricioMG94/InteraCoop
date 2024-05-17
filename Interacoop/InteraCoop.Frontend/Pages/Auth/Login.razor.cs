using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Frontend.Services;
using InteraCoop.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Auth
{
    public partial class Login
    {
        private LoginDto loginDto = new();
        private bool wasClose;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private ILoginService LoginService { get; set; } = null!;
        
       
        private async Task LoginAsync()
        {
            //if (wasClose)
            //{
            //    NavigationManager.NavigateTo("/");
            //    return;
            //}

            var responseHttp = await Repository.PostAsync<LoginDto, TokenDto>("/api/accounts/Login", loginDto);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            await LoginService.LoginAsync(responseHttp.Response!.Token);
            NavigationManager.NavigateTo("/");
        }
    }
}
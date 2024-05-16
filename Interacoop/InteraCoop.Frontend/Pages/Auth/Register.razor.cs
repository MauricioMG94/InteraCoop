using CurrieTechnologies.Razor.SweetAlert2;
using InteraCoop.Frontend.Repositories;
using InteraCoop.Frontend.Services;
using InteraCoop.Shared.Dtos;
using InteraCoop.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace InteraCoop.Frontend.Pages.Auth
{
    public partial class Register
    {
        private UserDto userDto= new();
        private bool loading;
        private string? imageUrl;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private ILoginService LoginService { get; set; } = null!;


        private void ImageSelected(string imagenBase64)
        {
            userDto.Photo = imagenBase64;
            imageUrl = null;
        }

        private async Task CreateUserAsync()
        {
            userDto.UserName = userDto.Email;
            userDto.UserType = UserType.Employee;
            loading = true;
            var responseHttp = await Repository.PostAsync<UserDto, TokenDto>("/api/accounts/CreateUser", userDto);
            loading = false;
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message,SweetAlertIcon.Error);
                return;
            }
            await LoginService.LoginAsync(responseHttp.Response!.Token);
            NavigationManager.NavigateTo("/");
        }
    }
}
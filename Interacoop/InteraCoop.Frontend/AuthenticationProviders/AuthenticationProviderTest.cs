using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace InteraCoop.Frontend.AuthenticationProviders
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var annonimous = new ClaimsIdentity();
            var user = new ClaimsIdentity(authenticationType: "test");
            var admin = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "Harold"),
                new Claim("LastName", "Aguirre"),
                new Claim(ClaimTypes.Name, "haguirre@gotmail.com"),
                new Claim(ClaimTypes.Role, "Admin"),
            }, authenticationType: "test");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(admin)));
        }
    }
}

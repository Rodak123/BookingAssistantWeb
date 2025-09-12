using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

public class FakeAuthStateProvider : AuthenticationStateProvider
{
    private bool _isLoggedIn = false;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsIdentity identity;

        if (_isLoggedIn)
        {
            identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "TestUser"),
            }, "FakeAuth");
        }
        else
        {
            identity = new ClaimsIdentity(); // empty = not logged in
        }

        var user = new ClaimsPrincipal(identity);
        return Task.FromResult(new AuthenticationState(user));
    }

    public void MarkUserAsLoggedIn()
    {
        _isLoggedIn = true;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void MarkUserAsLoggedOut()
    {
        _isLoggedIn = false;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}

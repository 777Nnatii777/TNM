using Microsoft.AspNetCore.Identity;

public interface IAccountService
{
    Task<IdentityResult> RegisterUserAsync(string username, string password);
    Task<SignInResult> LoginUserAsync(string username, string password);
    Task LogoutUserAsync();
}

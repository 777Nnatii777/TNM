using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterUserAsync(string username, string password)
    {
        var user = new IdentityUser { UserName = username };
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<SignInResult> LoginUserAsync(string username, string password)
    {
        return await _signInManager.PasswordSignInAsync(username, password, isPersistent: false, lockoutOnFailure: false);
    }

    public async Task LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }
}

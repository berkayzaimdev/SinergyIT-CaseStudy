namespace Identity.API.Features.Auth.Login;

public record LoginCommand
(
	string UserName,
	string Password
) : ICommand<LoginResult>;

public class LoginResult
{
	public bool IsSuccess { get; set; }
	public string Token { get; set; } = default!;
	public IEnumerable<string> Errors { get; set; } = [];
}

public class LoginHandler
	(ITokenService tokenService, UserManager<ApplicationUser> userManager)
	: ICommandHandler<LoginCommand, LoginResult>
{
	public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
	{
		var user = await userManager.FindByNameAsync(command.UserName);

        if (user is null)
        {
			return new LoginResult
			{
				IsSuccess = false,
				Errors = ["Girdiğiniz username'a sahip bir kullanıcı bulunamadı!"]
			};
		}

        bool succeeded = await userManager.CheckPasswordAsync(user, command.Password);

		if (!succeeded)
		{
			return new LoginResult
			{
				IsSuccess = false,
				Errors = ["Lütfen doğru bir username-şifre kombinasyonu giriniz!"]
			};
		}

		var claims = GetTokenClaims(user);

		string token = tokenService.GenerateAccessToken(claims);

		return new LoginResult
		{
			IsSuccess = true,
			Token = token
		};
	}

	private Claim[] GetTokenClaims(ApplicationUser user)
	{
		return
		[
			new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Name, user.UserName),
		];
	}
}

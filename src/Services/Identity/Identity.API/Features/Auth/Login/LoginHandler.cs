using Identity.API.Models;

namespace Identity.API.Features.Auth.Login;

public record LoginCommand
(
	string? UserName,
	string? Password
) : ICommand<LoginResult>;

public class LoginResult
{
	public bool IsSuccess { get; set; }
	public string Token { get; set; } = default!;
	public IEnumerable<string> Errors { get; set; } = [];
}

public class LoginHandler
	(UserManager<ApplicationUser> userManager)
	: ICommandHandler<LoginCommand, LoginResult>
{
	public async Task<LoginResult> Handle(LoginCommand command, CancellationToken cancellationToken)
	{
		if (string.IsNullOrEmpty(command.UserName) || string.IsNullOrEmpty(command.Password))
		{
			throw new ArgumentException(nameof(command));
		}

		var user = await userManager.FindByNameAsync(command.UserName);

        if (user is null)
        {
			throw new ArgumentException(nameof(command));
        }

        bool succeeded = await userManager.CheckPasswordAsync(user, command.Password);

		if (!succeeded)
		{
			return new LoginResult
			{
				IsSuccess = false,
				// Errors = identityResult.Errors
			};
		}

		string token = string.Empty;

		return new LoginResult
		{
			IsSuccess = true,
			Token = token,
		};
	}
}

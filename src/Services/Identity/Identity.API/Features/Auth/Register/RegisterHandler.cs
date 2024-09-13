using Identity.API.Models;

namespace Identity.API.Features.Auth.Register;

public record RegisterCommand
(
	string? FirstName,
	string? LastName,
	string? UserName,
	string? Password
) : ICommand<RegisterResult>;

public class RegisterResult
{
	public bool IsSuccess { get; set; }
	public string Token { get; set; } = default!;
	public IEnumerable<string> Errors { get; set; } = [];
}

public class RegisterHandler
	(UserManager<ApplicationUser> userManager)
	: ICommandHandler<RegisterCommand, RegisterResult>
{
	public async Task<RegisterResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
	{
        if (string.IsNullOrEmpty(command.FirstName) || string.IsNullOrEmpty(command.LastName) || string.IsNullOrEmpty(command.UserName) || string.IsNullOrEmpty(command.Password))
        {
			throw new ArgumentException(nameof(command));
        }

		var identityResult = await userManager.CreateAsync(new ApplicationUser() { FirstName = command.FirstName, LastName = command.UserName});

        if (!identityResult.Succeeded)
        {
			return new RegisterResult
			{
				IsSuccess = false,
				// Errors = identityResult.Errors
			};
        }

        return new RegisterResult();
	}

	//private bool AreParamsValid(RegisterCommand command)
	//{
	//	return string.IsNullOrEmpty(command.FirstName) || string.IsNullOrEmpty(command.LastName) || string.IsNullOrEmpty(command.UserName) || string.IsNullOrEmpty(command.Password);
	//}
}

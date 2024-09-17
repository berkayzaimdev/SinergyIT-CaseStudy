using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Features.Auth.Login;

public record LoginRequest
(
	string? UserName,
	string? Password
);

public record LoginResponse
(
	string AccessToken
);

public class LoginEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/auth/login/", async ([FromBody] LoginRequest request, ISender sender) =>
		{
			var command = request.Adapt<LoginCommand>();

			var result = await sender.Send(command);

	       if(!result.IsSuccess)
		   {
				return Results.BadRequest(result.Errors);
		   }

			return TypedResults.Ok(new LoginResponse(result.Token));
		});
	}
}

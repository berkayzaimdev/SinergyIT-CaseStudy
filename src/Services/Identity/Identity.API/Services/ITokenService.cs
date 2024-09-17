namespace Identity.API.Services;

public interface ITokenService
{
	string GenerateAccessToken(Claim[] claims);
}

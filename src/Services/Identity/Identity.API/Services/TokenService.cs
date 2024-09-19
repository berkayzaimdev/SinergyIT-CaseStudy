using Shared;

namespace Identity.API.Services;

public class TokenService : ITokenService
{
	private readonly JwtSettings _settings;
	private readonly SigningCredentials _signingCredentials;

	public TokenService(IOptions<JwtSettings> jwtSettings)
	{
		_settings = jwtSettings.Value;
		var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
		_signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
	}

    public string GenerateAccessToken(Claim[] claims)
	{
		var tokenOptions = new JwtSecurityToken(
			issuer: _settings.Issuer,
			audience: _settings.Audience,
			claims: claims,
			expires: DateTime.Now.AddMinutes(_settings.ExpirationInMinutes),
			signingCredentials: _signingCredentials
		);

		string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

		return tokenString;
	}
}
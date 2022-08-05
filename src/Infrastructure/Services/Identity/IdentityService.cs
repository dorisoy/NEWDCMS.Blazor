using DCMS.Application.Configurations;
using DCMS.Application.Interfaces.Services.Identity;
using DCMS.Application.Requests.Identity;
using DCMS.Application.Models.Identity;
using DCMS.Infrastructure.Models.Identity;
using DCMS.Shared.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DCMS.Infrastructure.Services.Identity
{
	public class IdentityService : ITokenService
	{
		private const string InvalidErrorMessage = "无效邮箱或者密码.";

		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly AppConfiguration _appConfig;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IStringLocalizer<IdentityService> _localizer;

		public IdentityService(
			UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
			IOptions<AppConfiguration> appConfig, SignInManager<AppUser> signInManager,
			IStringLocalizer<IdentityService> localizer)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_appConfig = appConfig.Value;
			_signInManager = signInManager;
			_localizer = localizer;
		}

		public async Task<Result<TokenModel>> LoginAsync(TokenRequest model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				return await Result<TokenModel>.FailAsync(_localizer["User Not Found."]);
			}

			if (!user.IsActive)
			{
				return await Result<TokenModel>.FailAsync(_localizer["User Not Active. Please contact the administrator."]);
			}

			if (!user.EmailConfirmed)
			{
				return await Result<TokenModel>.FailAsync(_localizer["E-Mail not confirmed."]);
			}

			var passwordValid = await _userManager.CheckPasswordAsync(user, model.Password);
			if (!passwordValid)
			{
				return await Result<TokenModel>.FailAsync(_localizer["Invalid Credentials."]);
			}

			user.RefreshToken = GenerateRefreshToken();
			user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
			await _userManager.UpdateAsync(user);

			var token = await GenerateJwtAsync(user);
			var response = new TokenModel 
			{ 
				Token = token, 
				RefreshToken = user.RefreshToken, 
				UserImageURL = user.ProfilePictureDataUrl
			};
			return await Result<TokenModel>.SuccessAsync(response);
		}

		public async Task<Result<TokenModel>> GetRefreshTokenAsync(RefreshTokenRequest model)
		{
			if (model is null)
			{
				return await Result<TokenModel>.FailAsync(_localizer["Invalid Client Token."]);
			}
			var userPrincipal = GetPrincipalFromExpiredToken(model.Token);
			var userEmail = userPrincipal.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(userEmail);
			if (user == null)
				return await Result<TokenModel>.FailAsync(_localizer["User Not Found."]);
			if (user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
				return await Result<TokenModel>.FailAsync(_localizer["Invalid Client Token."]);
			var token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user));
			user.RefreshToken = GenerateRefreshToken();
			await _userManager.UpdateAsync(user);

			var response = new TokenModel { Token = token, RefreshToken = user.RefreshToken, RefreshTokenExpiryTime = user.RefreshTokenExpiryTime };
			return await Result<TokenModel>.SuccessAsync(response);
		}

		private async Task<string> GenerateJwtAsync(AppUser user)
		{
			var token = GenerateEncryptedToken(GetSigningCredentials(), await GetClaimsAsync(user));
			return token;
		}

		private async Task<IEnumerable<Claim>> GetClaimsAsync(AppUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = new List<Claim>();
			var permissionClaims = new List<Claim>();
			foreach (var role in roles)
			{
				roleClaims.Add(new Claim(ClaimTypes.Role, role));
				var thisRole = await _roleManager.FindByNameAsync(role);
				var allPermissionsForThisRoles = await _roleManager.GetClaimsAsync(thisRole);
				permissionClaims.AddRange(allPermissionsForThisRoles);
			}

			var claims = new List<Claim>
			{
				new(ClaimTypes.NameIdentifier, user.Id),
				new(ClaimTypes.Email, user.Email?? string.Empty),
				new(ClaimTypes.Name, user.UserName?? string.Empty),
				new(ClaimTypes.Surname, user.UserRealName?? string.Empty),
				new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty)
			};

			if (userClaims.Any())
				claims.Union(userClaims);

			if (roleClaims.Any())
				claims.Union(roleClaims);

			if (permissionClaims.Any())
				claims.Union(permissionClaims);

			return claims;
		}

		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}

		private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
		{
			var token = new JwtSecurityToken(
			   claims: claims,
			   expires: DateTime.UtcNow.AddDays(2),
			   signingCredentials: signingCredentials);
			var tokenHandler = new JwtSecurityTokenHandler();
			var encryptedToken = tokenHandler.WriteToken(token);
			return encryptedToken;
		}

		private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appConfig.Secret)),
				ValidateIssuer = false,
				ValidateAudience = false,
				RoleClaimType = ClaimTypes.Role,
				ClockSkew = TimeSpan.Zero,
				ValidateLifetime = false
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
			if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
				StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException(_localizer["Invalid token"]);
			}

			return principal;
		}

		private SigningCredentials GetSigningCredentials()
		{
			var secret = Encoding.UTF8.GetBytes(_appConfig.Secret);
			return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
		}
	}
}
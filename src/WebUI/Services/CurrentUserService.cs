using Application.Common.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public readonly JWTSettings _jwtSetting;
        public CurrentUserService(IOptions<JWTSettings> jwtSetting,   IHttpContextAccessor httpContextAccessor)
        {
            _jwtSetting = jwtSetting.Value;
            // UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("sub");
        }

        public async Task<RefreshToken> GenerateEncodedToken(string id, string userRole, Domain.Entities.Roles roles)
        {
            var identity = GenerateClaimsIdentity(id, userRole);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userRole),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtSetting.JtiGenerator()),
                  new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtSetting.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                  identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol),
                 identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id)
            };
            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                _jwtSetting.Issuer,
                _jwtSetting.Audience,
                claims,
                _jwtSetting.NotBefore,
                _jwtSetting.Expiration,
                _jwtSetting.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new RefreshToken(identity.Claims.Single(c => c.Type == "id").Value, encodedJwt, (int)_jwtSetting.ValidFor.TotalSeconds);
        }

        private static long ToUnixEpochDate(DateTime date)
           => (long)Math.Round((date.ToUniversalTime() -
                                new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                               .TotalSeconds);
        private static ClaimsIdentity GenerateClaimsIdentity(string id, string userRole)
        {
            return new ClaimsIdentity(new GenericIdentity(userRole, "Token"), new[]
            {
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ApiAccess)
            });
        }
       

        public string UserId { get; }

       
    }
}
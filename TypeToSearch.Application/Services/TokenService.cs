using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TypeToSearch.Domain.Dtos.Responses;
using TypeToSearch.Domain.Entities;
using TypeToSearch.Domain.Interfaces.Services;

namespace TypeToSearch.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(string key, string iss, string aud)
        {
            _secretKey = key;
            _issuer = iss;
            _audience = aud;
        }

        public string GenJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.CdUser.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenHashPassword(string password) => ComputeSha256Hash(password);

        public bool VerifyHashPassword(User user, string password) =>
            user.PasswordHash == ComputeSha256Hash(password);

        public GenericResponse<object> ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var email = principal.FindFirst(ClaimTypes.Email)?.Value;

                    return new GenericResponse<object>
                    {
                        Content = new
                        {
                            UserId = userId,
                            Email = email
                        },
                        Msg = "Token válido",
                        StatusCode = 200,
                    };
                }

                throw new UnauthorizedAccessException("Token inválido ou expirado");
            }
            catch
            {
                throw new UnauthorizedAccessException("Token inválido ou expirado");
            }
        }

        private string ComputeSha256Hash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}

using JwtWebApi.Dtos;
using JwtWebApi.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace JwtWebApi.Models
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        public string CreateToken(string appSettingsToken)
        {
            List<Claim> claims = GetClaims();

            byte[] key1 = System.Text.Encoding.UTF8.GetBytes(appSettingsToken);
            var key = new SymmetricSecurityKey(key1);

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private List<Claim> GetClaims()
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Name, Username),
                new Claim(ClaimTypes.Role, "Admin")
            };
        }

        public bool HasRefreshToken(string? refreshToken)
        {
            return RefreshToken.Equals(refreshToken);
        }

        public bool IsPasswordOK(string password)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }

        public void SetRefreshToken(RefreshToken newRefreshToken)
        {
            RefreshToken = newRefreshToken.Token;
            TokenCreated = newRefreshToken.Created;
            TokenExpires = newRefreshToken.Expires;
        }

        public void SetPasswordHash(UserDto request)
        {
            using (var hmac = new HMACSHA512())
            {
                Username = request.Username;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.Password));
                PasswordSalt = hmac.Key;
            }
        }
    }
}

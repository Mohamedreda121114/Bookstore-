using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookSTOER.Data;
using BookSTOER.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookSTOER.NewFolder.repos
{
    public class LoginRepose : ILoginRepos
    {
        private readonly dbcontext dbcontext;
        private readonly token token;

        public LoginRepose(dbcontext dbcontext, IOptions<token> tokenOptions)
        {
            this.dbcontext = dbcontext;
            this.token = tokenOptions.Value;
        }


        public LoginResult Logen(Login login)
        {
            if (login == null)
            {
                return new LoginResult { Success = false, Message = "Login data is missing." };
            }

            var user = dbcontext.Set<User>().FirstOrDefault(x => x.Email == login.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.password, user.password))
            {
                return new LoginResult { Success = false, Message = "Invalid email or password." };
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.signkey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.FullName),
        new Claim(ClaimTypes.Role, user.Role ?? "Client")
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = credentials,
                Issuer = token.Issuer,
                Audience = token.Aidience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new LoginResult
            {
                Success = true,
                Message = "Login successful",
                Data = new
                {
                    user.Id,
                    user.Email,
                    user.FullName,
                    user.Address,
                    user.PhoneNumber,
                    user.image,
                    user.Orders,
                    accessToken
                }
            };
        }

    }
}

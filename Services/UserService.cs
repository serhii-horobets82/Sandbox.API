using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Evoflare.API.Auth.Models;
using Evoflare.API.Configuration;
using Evoflare.API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Evoflare.API.Services
{
    public interface IUserService
    {
        ApplicationUser Authenticate(string username, string password);
        IEnumerable<ApplicationUser> GetAll();
        ApplicationUser GetById(string id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<ApplicationUser> _users = new List<ApplicationUser>
        { 
            new ApplicationUser { Id = "a1", FirstName = "Admin", LastName = "User", UserName= "admin", PasswordHash = "AQAAAAEAACcQAAAAEJnm5crjpM+E5mzmbs3opE9Z8fT2bq8CqnjVzawGydz+0/Y4xNSsw5+fTkCP2nz6XQ=="},
            new ApplicationUser { Id = "a2", FirstName = "Normal", LastName = "User", UserName = "user", PasswordHash = "AQAAAAEAACcQAAAAEJnm5crjpM+E5mzmbs3opE9Z8fT2bq8CqnjVzawGydz+0/Y4xNSsw5+fTkCP2nz6XQ=="} 
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public ApplicationUser Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.UserName == "admin");

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            // return users without passwords
            return _users.Select(x => x);
        }

        public ApplicationUser GetById(string id) {
            var user = _users.FirstOrDefault(x => x.Id == id);

            if (user != null) 
                user.PasswordHash = null;

            return user;
        }
    }
}
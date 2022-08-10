using ClothingStoreAPI.Authentication;
using ClothingStoreAPI.Entities;
using ClothingStoreAPI.Entities.DbContextConfigure;
using ClothingStoreAPI.Services.Interfaces;
using ClothingStoreModels.Dtos.Create;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClothingStoreAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ClothingStoreDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSettings;

        public AccountService(ClothingStoreDbContext dbContext, IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.authenticationSettings = authenticationSettings;
        }

        public string GenerateJwt(LoginUserDto dto)
        {
            var user = dbContext
                .Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            //var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
            };

            if (!string.IsNullOrEmpty(user.Nationality))
            {
                claims.Add(new Claim("Nationality", user.Nationality));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Nationality = dto.Nationality,
                DateOfBirth = dto.DateOfBirth,
                RoleId = dto.RoleId,
            };

            var HashPassword = passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = HashPassword;

            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
        }

        
    }
}

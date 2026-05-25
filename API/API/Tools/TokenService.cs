using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Tools {

   /// <summary>
   /// class responsible for generating JWT tokens
   /// </summary>
   public class TokenService {

      private readonly IConfiguration _config;

      public TokenService(IConfiguration config) {
         _config = config;
      }

      /// <summary>
      /// the token generation method, 
      /// it receives an IdentityUser and returns a JWT token as a string
      /// </summary>
      /// <param name="user">the identity user for whom to generate a token</param>
      /// <returns>JWT Token as a string</returns>
      public string GenerateToken(IdentityUser user) {
         var jwtSettings = _config.GetSection("Jwt");
         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

         var claims = new[]
        {
           new Claim(JwtRegisteredClaimNames.Sub, user.Id),   // User ID
           new Claim(JwtRegisteredClaimNames.Email, user.Email),  // User Email - não será nulo pq é usado como UserName
           new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
       };

         var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(Convert.ToDouble(jwtSettings["ExpireHours"])),
            signingCredentials: creds
        );

         return new JwtSecurityTokenHandler().WriteToken(token);
      }
   }




}

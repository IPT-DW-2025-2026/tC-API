using API.Data;
using API.Models;
using API.Models.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]

public class AuthController:ControllerBase {

   /// <summary>
   /// reference to database of the application
   /// </summary>
   private readonly ApplicationDbContext _context;

   /// <summary>
   /// access to data related to users and roles
   /// </summary>
   private readonly UserManager<IdentityUser> _userManager;

   /// <summary>
   /// tool to signin a user and check if the password is correct
   /// </summary>
   private readonly SignInManager<IdentityUser> _signInManager;

   /// <summary>
   /// object that contains the configuration of the application,
   /// we will use it to get the JWT settings
   /// </summary>
   private readonly IConfiguration _config;

   public AuthController(
      ApplicationDbContext context,
      UserManager<IdentityUser> userManager,
      SignInManager<IdentityUser> signInManager,
      IConfiguration config) {
      _context = context;
      _userManager = userManager;
      _signInManager = signInManager;
      _config = config;
   }



   [AllowAnonymous]
   [HttpPost("login")]
   public async Task<IActionResult> Login([FromBody] LoginDTO login) {

      var user = await _userManager.FindByEmailAsync(login.Username);
      if(user == null) return Unauthorized();

      var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
      if(!result.Succeeded) return Unauthorized();

      var token = GenerateJwtToken(login.Username);

      return Ok(new { token });
   }


   /// <summary>
   /// Create a JWT token for the user with the given username
   /// </summary>
   /// <param name="username">the username of the user</param>
   /// <returns>the JWT token</returns>
   private string GenerateJwtToken(string username) {
      var claims = new[] {
         new Claim(ClaimTypes.Name, username)
     };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: _config["Jwt:Key"]));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
          issuer: _config["Jwt:Issuer"],
          audience: _config["Jwt:Audience"],
          claims: claims,
          expires: DateTime.Now.AddHours(2),
          signingCredentials: creds);

      return new JwtSecurityTokenHandler().WriteToken(token);
   }


}

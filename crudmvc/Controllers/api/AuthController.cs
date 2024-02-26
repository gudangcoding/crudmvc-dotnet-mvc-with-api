using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using crudmvc.Models; // Import dari model

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        // Logika otentikasi pengguna menggunakan UserManager dan SignInManager
        var user = await _userManager.FindByNameAsync(loginModel.Username);

        if (user != null)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

            if (result.Succeeded)
            {
                // Jika otentikasi berhasil, buat token JWT
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }
        }

        return Unauthorized(new { Message = "Invalid credentials" });
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        if (user == null || string.IsNullOrEmpty(user.UserName))
        {
            throw new ArgumentNullException(nameof(user), "User or username cannot be null or empty");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        //var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);
        var key = Encoding.UTF8.GetBytes(_configuration?["Jwt:Secret"] ?? throw new InvalidOperationException("Jwt:Secret configuration is missing."));


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserName) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}

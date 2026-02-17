using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using QwickGo.Core.Dto;
using QwickGo.Services.Implementations.Tokens;
using QwickGo.Services.Interfaces;

namespace qwick_go.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenServices _tokenServices;
    private readonly IAuthServices _authServices;

    public AuthController(TokenServices tokenServices, IAuthServices authServices)
    {
        _tokenServices = tokenServices;
        _authServices = authServices;
    }

    [HttpPost("google-signup")]
    public async Task<IActionResult> GoogleSignup([FromBody] GoogleRequest request)
    {
        try
        {
            var result =  await _authServices.GoogleSignup(request.Token);

            if(result.UserExist)
            {
                return Conflict(new{Message="User already exists", Email=result.Email});
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7),
                Path = "/"
            };
            Response.Cookies.Append("refreshToken", result.RefreshToken, cookieOptions);

            return Ok(new
            {
                Token = result.Token,
                data = result
            });
        }catch (Exception e)
        {
            return BadRequest(new {Error = e.Message});
        }
    }
}
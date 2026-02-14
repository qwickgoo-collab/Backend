using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using QwickGo.Core.Dto;
using QwickGo.Services.Implementations.Tokens;

namespace qwick_go.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenServices _tokenServices;

    public AuthController(TokenServices tokenServices)
    {
        _tokenServices = tokenServices;
    }

    [HttpPost("google-signup")]
    public async Task<IActionResult> GoogleSignup([FromBody] GoogleRequest request)
    {
        try
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(request.Token);
            string email = decodedToken.Claims["email"].ToString()!;
            Console.WriteLine(email);
            return Ok(new { Message = "User verified", Email = email });
        }catch (Exception e)
        {
            return BadRequest(new {Error = e.Message});
        }
    }
}
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

/*
Google Singup - Accepts a Firebase token from the client, verifies it with Firebase Admin SDK, and checks if a user with the corresponding email already exists in the database. If the user exists, it returns a conflict response. If not, it creates a new user record in the database with the information from the Firebase token, generates a JWT access token and a refresh token, and returns them to the client. The refresh token is also stored in an HTTP-only cookie for secure storage on the client side.
*/
    [HttpPost("google-signup")]
    public async Task<IActionResult> GoogleSignup([FromBody] GoogleRequest request)
    {
        try
        {
            var result =  await _authServices.GoogleSignup(request.Token);

            if(result.UserExist)
            {
                return Conflict(new GoogleRequestResponseDto{IsCreated = false, Message = "User already exists", Email = result.Email});
            }

            // var cookieOptions = new CookieOptions
            // {
            //     HttpOnly = true,
            //     Secure = true,
            //     SameSite = SameSiteMode.None,
            //     Expires = DateTime.UtcNow.AddDays(7),
            //     Path = "/"
            // };
            // Response.Cookies.Append("refreshToken", result.RefreshToken, cookieOptions);

            return Ok(new GoogleRequestResponseDto{IsCreated = true});

        }catch (Exception e)
        {
            return BadRequest(new {Error = e.Message});
        }
    }

/*
email Signup - Accepts user details (name, email, phone, and password) from the client, checks if a user with the provided email already exists in the database. If the user exists, it returns a conflict response. If not, it creates a new user record in the database with the provided information, generates a JWT access token and a refresh token, and returns them to the client. The refresh token is also stored in an HTTP-only cookie for secure storage on the client side.
*/
    [HttpPost("email-signup")]
    public async Task<IActionResult> EmailSignup([FromBody] EmailRequestDto request){
        try
        {
            var result = await _authServices.EmailSignup(request);
            if(result.UserExist)
            {
                return Conflict(new GoogleRequestResponseDto{IsCreated = false, Message = "User already exists", Email = result.Email});
            }
            // var cookieOptions = new CookieOptions
            // {
            //     HttpOnly = true,
            //     Secure = true,
            //     SameSite = SameSiteMode.None,
            //     Expires = DateTime.UtcNow.AddDays(7),
            //     Path = "/"
            // };
            // Response.Cookies.Append("refreshToken", result.RefreshToken, cookieOptions);
            return Ok(new GoogleRequestResponseDto{IsCreated = true});

        }catch(Exception e)
        {
            return BadRequest(new { Error = e.Message });
        }
    }
}
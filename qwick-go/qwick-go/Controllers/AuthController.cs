using Microsoft.AspNetCore.Mvc;

namespace qwick_go.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("signup")]
    public IActionResult SignUp()
    {
        return Ok("Hello world this is working");
    }
}
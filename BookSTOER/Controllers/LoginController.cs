using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookSTOER.Data;
using BookSTOER.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using BookSTOER.NewFolder.repos;

namespace BookSTOER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginRepose loginRepose;

        public LoginController(LoginRepose loginRepose )
        {
            this.loginRepose = loginRepose;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login( Login login )
        {
            return Ok(loginRepose.Logen(login));




        }

    }
}

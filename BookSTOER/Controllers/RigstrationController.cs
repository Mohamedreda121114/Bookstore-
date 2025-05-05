using BCrypt.Net;
using BookSTOER.Data;
using BookSTOER.Model;
using BookSTOER.repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BookSTOER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RigstrationController : ControllerBase

    {
        private readonly RigstrationRepos rigstrationRepos;

        public RigstrationController( RigstrationRepos rigstrationRepos )
        {
            this.rigstrationRepos = rigstrationRepos;
        }

        [HttpPost]
        public async Task<IActionResult> Rigstration ([FromForm] Rigstration rigstration)
        {
            return Ok(rigstrationRepos.Rigstration(rigstration));
        
        
        }
    }
}

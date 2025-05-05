using BookSTOER.repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSTOER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepos userRepos;

        public UsersController(UserRepos userRepos)
        {
            this.userRepos = userRepos;
        }
        [HttpGet]
        public async Task<IActionResult> Allusers()
        {
            var users = userRepos.GetUsers();
            return Ok(users);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var result = userRepos.RemoveUser(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Search(int id)
        {
            var user = userRepos.GetUser(id);
            return Ok(user);
        }

        [HttpPut("{id}/{role}")]
        public async Task<IActionResult> UpdateRole(int id, string role)
        {
            var result = userRepos.UpdateRole(id, role);
            return Ok(result);
        }

    }
}

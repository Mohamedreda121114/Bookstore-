using BookSTOER.Data;
using BookSTOER.Model;
using BookSTOER.NewFolder.repos;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;

namespace BookSTOER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Bookcontroller : ControllerBase
    {
       
        private readonly BookRepos bookRepos;

        public Bookcontroller( BookRepos bookRepos )
        {
          
            this.bookRepos = bookRepos;
        }

        [HttpGet]
        public async Task<IActionResult> AllBook()
        {
            return Ok(bookRepos.GetBooks());
        }
        [HttpPost]
        public async Task<IActionResult> ADDBook([FromForm] BookDTO book )
        {

           return  Ok(bookRepos.AddBook(book));

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> saerch( int id )
        {
            return Ok(bookRepos.GetBook(id));
          
        }
        [HttpPut]
        public async Task<IActionResult> update (Book book )
        {
            return Ok(bookRepos.UpdateBook(book));
        }

        [HttpDelete]
        public async Task<IActionResult> delete(int id )
        {
            return Ok(bookRepos.RemoveBook(id));
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BookSTOER.Model
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? image { get; set; }
        public string Description { get; set; }
       
        public string Title { get; set; }
        
        public string Author { get; set; }
    
        public int rate { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace library.Dtos
{
    public class BookReadDto
    {
        
        public int Id { get; set; }

        
        public string Name { get; set; }

        
        public string Author { get; set; }
    }
}

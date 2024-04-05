using System.ComponentModel.DataAnnotations;

namespace library.Dtos
{
    public class BookUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }
    }
}

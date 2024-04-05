using System.ComponentModel.DataAnnotations;

namespace library.Models
{
    public class Book
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }
    }
}

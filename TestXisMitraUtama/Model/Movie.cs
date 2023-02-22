using TestXisMitraUtama;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TestXisMitraUtama.Model
{
    public class Movie
    {
        [Key]
        public long Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Can't be empty")]
        public string Title { get; set; }
        [Range(1, 10, ErrorMessage = "Rating value must between 0 and 10")]
        public double Rating { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

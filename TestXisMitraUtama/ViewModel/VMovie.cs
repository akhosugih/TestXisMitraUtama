using System.ComponentModel.DataAnnotations;

namespace TestXisMitraUtama.ViewModel
{
    public class VMovie
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Can't be empty")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(1, 10, ErrorMessage = "Rating value must between 0 and 10")]
        public double Rating { get; set; }
        
        public IFormFile Image { get; set; }
    }
}

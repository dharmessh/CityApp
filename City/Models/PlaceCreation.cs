using System.ComponentModel.DataAnnotations;

namespace City.Models
{
    public class PlaceCreation
    {
        [Required(ErrorMessage = "Please Provide a Name Value")]
        [MaxLength(50)]
        public string PlaceName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? PlaceDescription { get; set; }
    }
}

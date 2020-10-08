
using System.ComponentModel.DataAnnotations;

namespace JwtProjeClint.Models
{
    public class ProductAdd
    {
        [Required(ErrorMessage = "Ad alanı boş geçilemez")]
        public string Name { get; set; }
    }
}

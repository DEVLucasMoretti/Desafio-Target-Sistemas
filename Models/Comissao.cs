using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Comissao
    {
        [Required]
        public string vendedor { get; set; }
        [Required]
        public double comissao { get; set; }
    }
}

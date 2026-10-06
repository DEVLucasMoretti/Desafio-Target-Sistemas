using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Venda
    {
        [Required(ErrorMessage = "Campo Vendedor não pode ser vazio!")]
        public string vendedor { get; set; }

        [Required(ErrorMessage = "Preencha o Valor maior que zero !")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public double valor { get; set; }


    }
}

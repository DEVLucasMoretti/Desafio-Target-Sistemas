using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Estoque
    {
        [Required(ErrorMessage = "Preencha o código maior que zero !")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public int codigoProduto { get; set; }

        [Required(ErrorMessage = "Campo descrição do produto não pode ser vazio!")]
        public string descricaoProduto { get; set; }

        [Required(ErrorMessage = "Preencha o campo Estoque")]
        public int estoque { get; set; }

        [Required(ErrorMessage = "Informe o tipo: Entrada ou Saida")]
        public string tipo { get; set; }

    }
}

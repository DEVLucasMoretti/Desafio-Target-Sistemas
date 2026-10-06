using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Juros
    {
        [Required(ErrorMessage = "Preencha o valor!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal valor { get; set; }

        [Required(ErrorMessage = "Preencha a data de vencimento!")]
        [DataFutura]
        public DateTime dataVencimento { get; set; }
    }

    public class DataFuturaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value == null)
                return ValidationResult.Success;

            DateTime data = Convert.ToDateTime(value);

            if (data.Date >= DateTime.Today)
                return new ValidationResult("A data de vencimento deve ser anterior à data atual, para calcular a multa dos dias em atraso!");

            return ValidationResult.Success;
        }
    }
}

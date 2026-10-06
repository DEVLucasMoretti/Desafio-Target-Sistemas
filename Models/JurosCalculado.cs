using System;

namespace Models
{
    public class JurosCalculado
    {
        public decimal valorOriginal { get; set; }
        public DateTime dataVencimento { get; set; }
        public DateTime dataHoje { get; set; }
        public int diasAtraso { get; set; }
        public decimal valorJuros { get; set; }
        public decimal valorTotal { get; set; }
    }
}

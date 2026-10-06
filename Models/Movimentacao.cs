namespace Models
{
    public class Movimentacao
    {
        public int id { get; set; }
        public int codigoProduto { get; set; }
        public string descricaoProduto { get; set; }
        public string tipo { get; set; }
        public int quantidade { get; set; }
        public int estoqueFinal { get; set; }
    }
}

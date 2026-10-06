using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repositories
{
    public class Estoque
    {
        readonly SqlConnection conn;
        readonly SqlCommand cmd;

        public Estoque(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }

        static List<Models.Estoque> saldo = new List<Models.Estoque>();
        static int proximoId = 1;

        public async Task<List<Models.Movimentacao>> MovimentaEstoque(List<Models.Estoque> movimentos)
        {
            List<Models.Movimentacao> retorno = new List<Models.Movimentacao>();

            foreach (var item in movimentos)
            {
                Models.Estoque produto = saldo.Find(p => p.codigoProduto == item.codigoProduto);

                if (produto == null)
                {
                    produto = new Models.Estoque
                    {
                        codigoProduto = item.codigoProduto,
                        descricaoProduto = item.descricaoProduto,
                        estoque = 0
                    };
                    saldo.Add(produto);
                }

                if (item.tipo.Trim().ToLower() == "entrada")
                    produto.estoque += item.estoque;
                else
                    produto.estoque -= item.estoque;

                retorno.Add(new Models.Movimentacao
                {
                    id = proximoId++,
                    codigoProduto = produto.codigoProduto,
                    descricaoProduto = produto.descricaoProduto,
                    tipo = item.tipo,
                    quantidade = item.estoque,
                    estoqueFinal = produto.estoque
                });
            }

            return retorno;
        }


    }
}


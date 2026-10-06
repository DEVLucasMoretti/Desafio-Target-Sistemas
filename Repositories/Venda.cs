using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Repositories
{
    public class Venda
    {
        readonly SqlConnection conn;
        readonly SqlCommand cmd;

        public Venda(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }

        public async Task<List<Models.Comissao>> CalculaComissaoVenda(List<Models.Venda> vendas)
        {
            double valorComissao;
            List<Models.Comissao> vendasComissao = new List<Models.Comissao>();

              foreach (var item in vendas)
            {
                
                if (item.valor  < 100)
                {
                    valorComissao = 0;
                }
                else if (item.valor >= 100 && item.valor < 500)
                {
                    valorComissao = item.valor * 0.01;
                }
                else
                {
                    valorComissao = item.valor * 0.05;
                }

                Models.Comissao comissao = null;
                foreach (var c in vendasComissao)
                {
                    if (c.vendedor == item.vendedor)
                    {
                        comissao = c;
                        break;
                    }
                }

                if (comissao == null)
                {
                    comissao = new Models.Comissao();
                    comissao.vendedor = item.vendedor;
                    comissao.comissao = valorComissao;
                    vendasComissao.Add(comissao);
                }
                else
                {
                    comissao.comissao += valorComissao;
                }
            }

            foreach (var c in vendasComissao)
                c.comissao = Math.Round(c.comissao, 2);

            return  vendasComissao;
        }
    }

}

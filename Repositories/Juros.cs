using System;
using System.Data.SqlClient;

namespace Repositories
{
    public class Juros
    {
        readonly SqlConnection conn;
        readonly SqlCommand cmd;

        public Juros(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            cmd = new SqlCommand();
            cmd.Connection = conn;
        }

        const decimal TAXA_DIA = 0.025m;   // 2,5% ao dia

        public Models.JurosCalculado CalculaJuros(Models.Juros item)
        {
            DateTime hoje = DateTime.Today;

            int dias = (hoje - item.dataVencimento.Date).Days;

            if (dias < 0)
                dias = 0;   // ainda não venceu

            decimal juros = item.valor * TAXA_DIA * dias;

            return new Models.JurosCalculado
            {
                valorOriginal = item.valor,
                dataVencimento = item.dataVencimento.Date,
                dataHoje = hoje,
                diasAtraso = dias,
                valorJuros = Math.Round(juros, 2),
                valorTotal = Math.Round(item.valor + juros, 2)
            };
        }
    }
}

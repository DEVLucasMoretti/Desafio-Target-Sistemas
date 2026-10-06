using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace desafio_target_sistemas.Controllers
{
    public class VendasController : ApiController
    {
        readonly Utils.Logger logger;
        readonly Repositories.Venda repository;

        public VendasController()
        {
            logger = new Utils.Logger(Configurations.Config.GetLogPath());
            repository = new Repositories.Venda(Configurations.Config.GetConnection());
        }

        // POST: api/Vendas
        public async Task<IHttpActionResult> Post([FromBody] Models.VendaRequest vendas)
        {
            if (vendas == null)
                return BadRequest("Os dados das vendas não foram enviadas");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await repository.CalculaComissaoVenda(vendas.vendas));
            }
            catch (Exception ex)
            {
                await logger.Log(ex);
                return InternalServerError();
            }
        }
    }
}

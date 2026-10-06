using System;
using System.Threading.Tasks;
using System.Web.Http;
using Utils;

namespace desafio_target_sistemas.Controllers
{
    public class EstoquesController : ApiController
    {
        readonly Utils.Logger logger;
        readonly Repositories.Estoque repository;

        public EstoquesController()
        {
            logger = new Utils.Logger(Configurations.Config.GetLogPath());
            repository = new Repositories.Estoque(Configurations.Config.GetConnection());
        }

        // Post: api/Estoques
        public async Task<IHttpActionResult> Post([FromBody] Models.EstoqueRequest estoque)
        {
            if (estoque == null)
                return BadRequest("Os dados das vendas não foram enviadas");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await repository.MovimentaEstoque(estoque.estoque));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}


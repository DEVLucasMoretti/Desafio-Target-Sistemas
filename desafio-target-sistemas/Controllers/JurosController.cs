using System;
using System.Threading.Tasks;
using System.Web.Http;
namespace desafio_target_sistemas.Controllers
{
    public class JurosController : ApiController
    {
        readonly Utils.Logger logger;
        readonly Repositories.Juros repository;

        public JurosController()
        {
            logger = new Utils.Logger(Configurations.Config.GetLogPath());
            repository = new Repositories.Juros(Configurations.Config.GetConnection());
        }
        public async Task<IHttpActionResult> Post([FromBody] Models.Juros juros)
        {
            if (juros == null)
                return BadRequest("Os dados não foram enviados");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(repository.CalculaJuros(juros));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

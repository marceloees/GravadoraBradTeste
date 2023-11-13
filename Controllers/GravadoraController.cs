using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace GravadoraBradTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GravadoraController : ControllerBase, IGrupos
    {
        private readonly IConfiguration _config;

        public GravadoraController(IConfiguration config)
        {
            _config = config;   
        }

        [HttpPost]
        public async Task<ActionResult<GruposMusicais>> cadastrarGrupos()
        {
            try
            {
                    var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                    await connection.ExecuteAsync("Insert into Grupos (Nome, Integrantes, Ritmo values (@NomeBanda, @Integrantes, @Ritmo))");
                    return Ok(await ListarGrupos(connection));
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
            
        }

        [HttpPut]
        public async Task<ActionResult<GruposMusicais>> AtualizarGrupo(GruposMusicais gm)
        {
            try
            {
                var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                await connection.ExecuteAsync("update Grupos set Nome = @NomeBanda, Integrantes = @Integrantes, Ritmo = @Ritmo", gm);
                return Ok(await ListarGrupos(connection));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return BadRequest();
            }
            
        }

        [HttpDelete("{IdBanda}")]
        public async Task<ActionResult<List<GruposMusicais>>> ExcluirGrupo(int gm)
        {
            try
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                await connection.ExecuteAsync("delete from Grupos where IdBanda = @IdBanda", new { IdBanda = gm });
                return Ok(await ListarGrupos(connection));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return BadRequest();
            }
            
        }

        private static async Task<IEnumerable<GruposMusicais>> ListarGrupos(SqlConnection connection)
        {
            return await connection.QueryAsync<GruposMusicais>("select * from Grupos");
        }
    }
}

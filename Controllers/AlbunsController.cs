using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GravadoraBradTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbunsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AlbunsController(IConfiguration config)
        {
            _config = config;
        }

        
        // POST api/<AlbunsController>
        [HttpPost]
        public async Task<ActionResult<Albuns>> cadastrarAlbuns()
        {
            try
            {
                //Ao cadastrar o album, a ideia é pegar o nome que está na tabela Grupos e Inserir em Albuns
                var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                await connection.ExecuteAsync("Insert into Albuns (NomeAlbum, (SELECT idBanda from Grupos where Nome = @Artista), Ritmo " +
                    "values (@NomeAlbum, @Artista, @Ritmo))");
                return Ok(await ListarAlbuns(connection));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }

        }

        [HttpPut]
        public async Task<ActionResult<GruposMusicais>> AtualizarAlbuns(Albuns albuns)
        {
            try
            {
                var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                await connection.ExecuteAsync("update Albuns set NomeAlbum = @NomeAlbum, Ritmo = @Ritmo " +
                    "where IdBanda in(SELECT IdBanda from Grupos where NomeBanda = @Artista)", albuns);
                return Ok(await ListarAlbuns(connection));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return BadRequest();
            }

        }

        [HttpDelete("{IdAlbum}")]
        public async Task<ActionResult<List<GruposMusicais>>> ExcluirAlbuns(int albumId)
        {
            try
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                await connection.ExecuteAsync("delete from Albuns where IdAlbum = @IdAlbum", new { IdAlbum = albumId });
                return Ok(await ListarAlbuns(connection));
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return BadRequest();
            }

        }

        private static async Task<IEnumerable<GruposMusicais>> ListarAlbuns(SqlConnection connection)
        {
            return await connection.QueryAsync<GruposMusicais>("select * from Albuns");
        }
    }
}

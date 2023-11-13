using Microsoft.AspNetCore.Mvc;

namespace GravadoraBradTeste
{
    public interface IGrupos
    {
        public Task<ActionResult<GruposMusicais>> cadastrarGrupos();

        public Task<ActionResult<GruposMusicais>> AtualizarGrupo(GruposMusicais gm);

        public Task<ActionResult<List<GruposMusicais>>> ExcluirGrupo(int gm);


    }
}

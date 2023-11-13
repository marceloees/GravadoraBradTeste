using System.ComponentModel.DataAnnotations;

namespace GravadoraBradTeste
{
    public class GruposMusicais
    {
        [Key]
        [Required]
        public int IdBanda { get; set; }
        [Required]
        public string NomeBanda { get; set; }
        [Required]
        public int Integrantes { get; set; }
        [Required]
        public string Ritmo { get; set; }


    }
}

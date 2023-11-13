using System.ComponentModel.DataAnnotations;

namespace GravadoraBradTeste
{
    public class Albuns
    {
        [Key]
        [Required]
        public int IdAlbum { get; set; }
        [Required]
        public string NomeAlbum { get; set; }
        [Required]
        public GruposMusicais Artista { get; set; }
        [Required]
        public string Ritmo { get; set; }
    }
}

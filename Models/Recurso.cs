using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiChamba.Models
{
    [Table("RECURSO")]
    public class Recurso
    {
        [Key]
        [Column("ID_RECURSO")]
        public int IdRecurso { get; set; }

        [Column("TITULO")]
        public string Titulo { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("URL")]
        public string Url { get; set; }

        [Column("IMAGEN")]
        public string Imagen { get; set; }
    }
}

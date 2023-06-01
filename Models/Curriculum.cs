using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiChamba.Models
{
    [Table("CURRICULUM")]
    public class Curriculum
    {

        public Curriculum() {
            Usuario= new Usuario();
        }

        [Key]
        [Column("ID_CURRICULUM")]
        public int IdCurriculum { get; set; }

        [Required]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("NOMBRE_ARCHIVO")]
        public string NombreArchivo { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MiChamba.Models
{
    [Table("CURRICULUM")]
    public class Curriculum
    {

        [Key]
        [Column("ID_CURRICULUM")]
        public int IdCurriculum { get; set; }

        [Required]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("NOMBRE_ARCHIVO")]
        public string? NombreArchivo { get; set; }


        [NotMapped]
        public IFormFile PdfFile { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiChamba.Models
{
    [Table("CALIFICACION")]
    public class Calificacion
    {
        [Key]
        [Column("ID_CALIFICACION")]
        public int IdCalificacion { get; set; }

        [Column("ID_EMPRESA")]
        public int IdEmpresa { get; set; }
        public Empresa Empresa { get; set; }

        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }

        [Column("CALIFICACION")]
        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
        public int calificacion { get; set; }

        [Column("COMENTARIO")]
        public string Comentario { get; set; }

        [Column("FECHA_CALIFICACION", TypeName = "datetime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaCalificacion { get; set; }

    }
}

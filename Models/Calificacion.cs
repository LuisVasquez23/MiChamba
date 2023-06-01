using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiChamba.Models
{
    [Table("CALIFICACION")]
    public class Calificacion
    {
        public Calificacion() {
            Empresa = new Empresa();
            Usuario = new Usuario();
        }

        [Key]
        [Column("ID_CALIFICACION")]
        public int IdCalificacion { get; set; }

        [Column("ID_EMPRESA")]
        [Required]
        public int IdEmpresa { get; set; }

        [Column("ID_USUARIO")]
        [Required]
        public int IdUsuario { get; set; }


        [Column("CALIFICACION")]
        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
        public int ValorCalificacion { get; set; }

        [Column("COMENTARIO")]
        public string Comentario { get; set; }

        [Column("FECHA_CALIFICACION", TypeName = "datetime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaCalificacion { get; set; }

        // Propiedades de navegacion
        [ForeignKey("IdEmpresa")]
        public virtual Empresa Empresa { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

    }
}

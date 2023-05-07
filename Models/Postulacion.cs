using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiChamba.Models
{
    [Table("POSTULACION")]
    public class Postulacion
    {
        [Key]
        [Column("ID_POSTULACION")]
        public int IdPostulacion { get; set; }

        // LLaves foraneas
        [Column("ID_OFERTA")]
        [ForeignKey("ID_OFERTA")]
        public int IdOferta { get; set; }
        public Oferta Oferta { get; set; }

        [Column("ID_USUARIO")]
        [ForeignKey("ID_USUARIO")]
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }


        [Column("FECHA_POSTULACION", TypeName = "datetime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaPostulacion { get; set; }

        [Column("ESTADO_POSTULACION")]
        public string EstadoPostulacion { get; set; }

    }
}

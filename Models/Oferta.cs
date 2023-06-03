using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiChamba.Models
{
    [Table("OFERTA")]
    public class Oferta
    {

        [Key]
        [Column("ID_OFERTA")]
        public int IdOferta { get; set; }

        [Column("TITULO")]
        [Required(ErrorMessage = "El título de la oferta es requerido.")]
        public string Titulo { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("REQUISITOS")]
        public string Requisitos { get; set; }

        [Column("UBICACION")]
        public string Ubicacion { get; set; }

        [Column("FECHA_PUBLICACION", TypeName = "datetime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaPublicacion { get; set; }


        [Column("ID_EMPRESA")]
        public int IdEmpresa { get; set; }


        [NotMapped]
        public String Educacion { get; set; }

        [NotMapped]
        public String Experiencia { get; set; }

        [NotMapped]
        public String Habilidades { get; set; }

    }
}

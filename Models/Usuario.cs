using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiChamba.Models
{
    [Table("USUARIO")]
    public class Usuario
    {

        [Key]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("NOMBRE")]
        [Required(ErrorMessage = "El nombre del usuario es requerido.")]
        public string Nombre { get; set; }

        [Column("APELLIDO")]
        public string Apellido { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("IMAGEN")]
        public string Imagen { get; set; }

        [Column("PASSWORD")]
        public string Password { get; set; }

        [Column("TELEFONO")]
        [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "El formato del teléfono no es válido.")]
        public string Telefono { get; set; }

        public ICollection<Postulacion> Postulaciones { get; set; }
        public ICollection<Calificacion> Calificaciones { get; set; }

    }
}

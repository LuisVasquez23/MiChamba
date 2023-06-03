using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiChamba.Models
{
    [Table("USUARIO")]
    public class Usuario
    {

        public Usuario() {
            Postulaciones = new List<Postulacion>();
            Calificaciones = new List<Calificacion>();
        }

        [Key]
        [Column("ID_USUARIO")]
        public int IdUsuario { get; set; }

        [Column("NOMBRE")]
        [Required(ErrorMessage = "El nombre del usuario es requerido.")]
        public string? Nombre { get; set; }

        [Column("APELLIDO")]
        [Required(ErrorMessage = "El apellido del usuario es requerido.")]
        public string? Apellido { get; set; }

        [Column("EMAIL")]
        [Required(ErrorMessage = "El email del usuario es requerido.")]
        public string? Email { get; set; }

        [Column("PAIS")]
        public string? Pais { get; set; }

        [Column("ESTADO")]
        public string? Estado { get; set; }

        [Column("IMAGEN")]
        public string Imagen { get; set; }

        [Column("PASSWORD")]
        [Required(ErrorMessage = "La contraseña del usuario es requerida.")]
        public string Password { get; set; }

        [Column("TELEFONO")]
        [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "El formato del teléfono no es válido. Tiene que ser 1234-1234")]
        public string Telefono { get; set; }

        [NotMapped] 
        public IFormFile ImagenFile { get; set; }

        public virtual ICollection<Postulacion> Postulaciones { get; set; }
        public virtual ICollection<Calificacion> Calificaciones { get; set; }

    }
}

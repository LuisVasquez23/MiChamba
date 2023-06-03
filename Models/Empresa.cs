using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiChamba.Models
{
    [Table("EMPRESA")]
    public class Empresa
    {

        [Key]
        [Column("ID_EMPRESA")] 
        public int IdEmpresa { get; set; }

        [Column("NOMBRE")]
        [Required(ErrorMessage = "El nombre de la empresa es requerido.")]
        public string Nombre { get; set; }

        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }

        [Column("DIRECCION")]
        public string Direccion { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("PASSWORD")]
        [Required(ErrorMessage = "La contraseña del usuario es requerida.")]
        public string Password { get; set; }

        [Column("IMAGEN")]
        public string Imagen { get; set; }

        [NotMapped]
        public IFormFile ImagenFile { get; set; }

        [NotMapped]
        public string? Estado { get; set; }

        [NotMapped]
        public string? Departamento { get; set; }

        [Column("TELEFONO")]
        [RegularExpression(@"^\d{4}-\d{4}$", ErrorMessage = "El formato del teléfono no es válido.")]
        public string Telefono { get; set; }


    }
}

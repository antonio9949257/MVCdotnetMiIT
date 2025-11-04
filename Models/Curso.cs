using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiIT.Models
{
    public class Curso
    {
        [Key]
        [Column("IIDCURSO")]
        public int IdCurso { get; set; }

        [Column("NOMBRE")]
        public string? Nombre { get; set; }

        [Column("DESCRIPCION")]
        public string? Descripcion { get; set; }

        [Column("BHABILITADO")]
        public int Bhabilitado { get; set; }
    }
}
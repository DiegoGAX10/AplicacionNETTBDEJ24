using System.ComponentModel.DataAnnotations;

namespace AplicacionNETTBDEJ24.Models
{
    public class Alumno
    {
        [Key]
        public int nualu { get; set; }

        public string nombre { get; set; } = null!;

        [Range(18, 65, ErrorMessage = "La edad del alumno debe estar entre 18 y 65 años.")]
        public int edad { get; set; } 

        public int sem { get; set; }

        public string espe { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace AplicacionNETTBDEJ24.Models
{
    public class Profesor
    {
        [Key]
        public int nuprof { get; set; }

        public string nombre { get; set; } = null!;

        public int sueldo { get; set; }

        public string grado { get; set; } = null!;

    }
}

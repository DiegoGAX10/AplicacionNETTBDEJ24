using System.ComponentModel.DataAnnotations;

namespace AplicacionNETTBDEJ24.Models
{
    public class Materias
    {
        [Key]
        public int numat { get; set; }

        public string nombre { get; set; } = null!;

        public int credi { get; set; }

        public int nuprof { get; set; }

    }
}

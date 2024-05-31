using System.ComponentModel.DataAnnotations;

namespace AplicacionNETTBDEJ24.Models
{
    public class Profesor
    {
        [Key]
        public int nuprof { get; set; }

        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y tildes.")]
        public string nombre { get; set; } = null!;

        [Range(0, int.MaxValue, ErrorMessage = "El sueldo debe ser un valor positivo.")]
        public int sueldo { get; set; }

        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El grado solo puede contener letras y tildes.")]
        public string grado { get; set; } = null!;
    }
}


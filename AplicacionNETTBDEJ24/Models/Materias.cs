using System.ComponentModel.DataAnnotations;

namespace AplicacionNETTBDEJ24.Models
{
    public class Materias
    {
        [Key]
        public int numat { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-Z\sáéíóúÁÉÍÓÚñÑ]+$", ErrorMessage = "El nombre solo puede contener letras y tildes.")]
        public string nombre { get; set; } = null!;

        [Range(1, int.MaxValue, ErrorMessage = "El número de créditos debe ser un valor positivo.")]
        public int credi { get; set; }

        [Required(ErrorMessage = "El número de profesor es obligatorio.")]
        public int nuprof { get; set; }
    }
}

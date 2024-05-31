using System.ComponentModel.DataAnnotations;

namespace AplicacionNETTBDEJ24.Models
{
    public class Kardex
    {
        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "El numero de alumno debe ser  positivo.")]
        public int nualu { get; set; }

        public int numat { get; set; }

        [Range(0, 100, ErrorMessage = "La calificacion debe de estar entre 0 y 100.")]
        public int calif { get; set; }

    }
}

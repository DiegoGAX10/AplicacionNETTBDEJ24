using System.ComponentModel.DataAnnotations;

namespace AplicacionNETTBDEJ24.Models
{
    public class Kardex
    {
        [Key]
        public int nualu { get; set; }

        public int numat { get; set; }

        [Range(0, 100, ErrorMessage = "La calificacion debe de estar entre 0 y 100.")]
        public int calif { get; set; }

    }
}

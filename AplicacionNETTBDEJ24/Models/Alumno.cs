using System.ComponentModel.DataAnnotations;

namespace AplicacionNETTBDEJ24.Models
{
    public class Alumno
    {
        [Key]
        public int nualu { get; set; }

        public string nombre { get; set; } = null!;

        public int edad { get; set; } 

        public int sem { get; set; }

        public string espe { get; set; } = null!;
    }
}

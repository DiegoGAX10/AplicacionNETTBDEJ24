using System.ComponentModel.DataAnnotations;

namespace AplicacionNETTBDEJ24.Models
{
    public class Kardex
    {
        [Key]
        public int nualu { get; set; }

        public int numat { get; set; }

        public int calif { get; set; }

    }
}

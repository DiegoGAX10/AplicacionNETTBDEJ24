﻿using System.ComponentModel.DataAnnotations;

namespace AplicacionNETTBDEJ24.Models
{
    public class Alumno
    {
        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "El numero de alumno debe ser positivo.")]
        public int nualu { get; set; }

        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y tildes.")]
        public string nombre { get; set; } = null!;

        [Range(18, 65, ErrorMessage = "La edad del alumno debe estar entre 18 y 65 años.")]
        public int edad { get; set; }

        [Range(1, 14, ErrorMessage = "El número del semestre debe ser un valor positivo y entre 1 a 14 ")]
        public int sem { get; set; }

        public string espe { get; set; } = null!;
    }
}

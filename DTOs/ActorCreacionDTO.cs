using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using api.Validaciones;

namespace api.DTOs
{
    public class ActorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(150, ErrorMessage = "El campo {0} debe tener {1} caracteres o menos")]
        [PrimeraLetraMayuscula]
        public required string Nombre { get; set; }

        [FechaNoPuedeSerFutura]
        public DateOnly FechaNacimiento { get; set; }

        [ImagenValidacion(4)]
        public IFormFile? Foto { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Cervantes.Arquisoft.View.Models
{
    public class InformeViewModel
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 50, MinimumLength = 10, ErrorMessage = "La longitud de {0} debe estar entre {1} y {2}")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
    }
}

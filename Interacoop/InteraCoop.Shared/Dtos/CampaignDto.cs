using InteraCoop.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace InteraCoop.Shared.Dtos
{
    public class CampaignDto
    {
        public int Id { get; set; }

        [Display(Name = "Identificador de campaña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string CampaignId { get; set; } = null!;

        [Display(Name = "Nombre de campaña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string CampaignName { get; set; } = null!;

        [Display(Name = "Tipo de campaña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string CampaignType { get; set; } = null!;

        [Display(Name = "Estado de campaña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Status { get; set; } = null!;

        [Display(Name = "Descripción")]
        [MaxLength(300, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Description { get; set; } = null!;

        [Display(Name = "Fecha de inicio")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha de fin")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        //[DateGreaterThan(nameof(StartDate), ErrorMessage = "La fecha de fin debe ser posterior a la fecha de inicio.")]
        public DateTime EndDate { get; set; }

        public List<int>? ProductsIds { get; set; }
    }
}

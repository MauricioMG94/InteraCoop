using InteraCoop.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace InteraCoop.Shared.Dtos
{
    public class OpportunityDto
    {
        public int Id
        { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string Status
        { get; set; } = null!;

        [Display(Name = " Observaciones de la oportunidad")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(250, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string OpportunityObservations
        { get; set; } = null!;

        [Display(Name = "Fecha de registro")]
        [DataType(DataType.Date)]
        public DateTime RecordDate
        { get; set; }

        [Display(Name = "Fecha estimada de adquisición")]
        [DataType(DataType.Date)]
        public DateTime EstimatedAcquisitionDate
        { get; set; }
        public int CampaignId { get; set; }
        public int InteractionId { get; set; }

    }
}

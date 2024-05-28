using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace InteraCoop.Shared.Entities
{
    public class Opportunity
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

        [Display(Name = "Campaña")]
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; } = null!;

        [Display(Name = "Interacción")]
        public int InteractionId { get; set; }
        public Interaction Interaction { get; set; } = null!;

    }
}

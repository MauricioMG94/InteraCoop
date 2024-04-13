using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraCoop.Shared.Entities
{
    public class Opportunity
    {
        public int Id
        { get; set; }

        [Display(Name = "Id de interacción")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int InteractionId
        { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string State
        { get; set; } = null!;

        [Display(Name = " Observaciones de la oportunidad")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string ObservationsOpportunity
        { get; set; } = null!;

        [Display(Name = "Fecha de registro")]
        [DataType(DataType.Date)]
        public DateTime RecordDate
        { get; set; }

        [Display(Name = "Fecha estimada de adquisición")]
        [DataType(DataType.Date)]
        public DateTime EstimatedAcquisitionDate
        { get; set; }

        [Display(Name = "Id de campaña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int CampaignId
        { get; set; }

        [Display(Name = "Fecha de auditoría")]
        [DataType(DataType.Date)]
        public DateTime AuditDate
        { get; set; }

        [Display(Name = "Usuario de auditoría")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string AuditUser
        { get; set; } = null!;

    }
}

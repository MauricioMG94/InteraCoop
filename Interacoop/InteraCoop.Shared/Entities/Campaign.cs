using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraCoop.Shared.Entities
{
    public class Campaign
    {
        public int Id { get; set; }

        [Display(Name = "Identificador de producto")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string ProductId { get; set; } = null!;

        [Display(Name = "Nombre de la campaña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string CampaignName { get; set; } = null!;

        [Display(Name = "Tipo de campaña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string CampaignType { get; set; } = null!;

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Status { get; set; } = null!;

        [Display(Name = "Descripcion")]
        [MaxLength(300, ErrorMessage = "La {0} excede el maximo de {1} caracteres.")]
        public string? Description { get; set; }

        [Display(Name = "Fecha de inicio")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha de fin")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Fecha de auditoria")]
        [Required(ErrorMessage = "La {0} es requerida.")]
        public DateTime AuditDate { get; set; }

        [Display(Name = "Usuario de auditoria")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string AuditUser { get; set; } = null!;

    }
}

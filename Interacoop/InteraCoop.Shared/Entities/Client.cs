using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraCoop.Shared.Entities
{
    public class Client
    {
        public int Id { get; set; }

        [Display(Name = "Ciudad")]
        //[Required(ErrorMessage = "El campo {0} es requerido.")]
        public int City { get; set; }

        [Display(Name = "Nombre cliente")]
        [MaxLength(150, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
       // [Required(ErrorMessage = "El campo {0} es requerido.")]
        public String Name { get; set; } = null!;

        [Display(Name = "Documento")]
       // [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Document { get; set; }

        [Display(Name = "Tipo documento")]
        //[Required(ErrorMessage = "El campo {0} es requerido.")]
        public String DocumentType { get; set; } = null!;

        [Display(Name = "Telefono")]
        //[Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Telephone { get; set; }

        [Display(Name = "Direccion")]
        //[Required(ErrorMessage = "El campo {0} es requerido.")]
        public String Address { get; set; } = null!;

        public DateTime RegistryDate { get; set; }
        public DateTime AuditUpdate { get; set; }
        public String AuditUser { get; set; } = null!;
    }
}

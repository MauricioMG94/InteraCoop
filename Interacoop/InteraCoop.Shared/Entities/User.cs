using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraCoop.Shared.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Nombre de usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string UserName { get; set; } = null!;

        [Display(Name = "Código de usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(10, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string UserCode { get; set; } = null!;

        [Display(Name = "Email de usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string UserEmail { get; set; } = null!;

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(12, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        public string UserPassword { get; set; } = null!;

        [Display(Name = "Usuario auditoría")]
        public string AuditUser { get; set; } = null!;

        [Display(Name = "Fecha de auditoría")]
        [DataType(DataType.Date)]
        public DateTime AuditDate { get; set; } = DateTime.Now;
    }
}

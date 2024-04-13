using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraCoop.Shared.Entities
{
    public class UserRol
    {
        public int Id { get; set; }

        [Display(Name = "Id de usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Display(Name = "Id de rol")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int RolId { get; set; }
        public Rol Rol { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraCoop.Shared.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de producto")]
        public string ProductType { get; set; } = null!;

        [Display(Name = "Nombre de producto")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;

        [Display(Name = "Cupo")]
        public int Quota { get; set; }

        [Display(Name = "Plazo")]
        public string? Term { get; set; }

        [Display(Name = "Valor")]
        public double? Value { get; set; }

        [Display(Name = "Tasa")]
        public double? Rate { get; set; }
    }
}

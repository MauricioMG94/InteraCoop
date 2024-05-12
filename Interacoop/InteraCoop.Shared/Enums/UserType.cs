using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraCoop.Shared.Enums
{
    public enum UserType
    {
        [Description("Administrador")]
        Admin,
        [Description("Empleado")]
        Employee,
        [Description("Analista")]
        Analist
    }
}

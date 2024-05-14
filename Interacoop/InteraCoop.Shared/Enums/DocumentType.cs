using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraCoop.Shared.Enums
{
   public enum DocumentType
    {
        [Description("Cedula")]
        CC,
        [Description("Tarjeta de identidad")]
        TI,
        [Description("Cedula de extranjeria")]
        CE,
        [Description("Registro civil")]
        RC,
        [Description("Registro unico tributario")]
        RUT
    }
}

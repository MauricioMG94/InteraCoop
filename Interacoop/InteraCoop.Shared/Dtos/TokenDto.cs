using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraCoop.Shared.Dtos
{
    public class TokenDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration {  get; set; }
    }
}

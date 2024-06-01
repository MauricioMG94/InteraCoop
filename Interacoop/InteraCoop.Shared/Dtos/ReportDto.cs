using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteraCoop.Shared.Dtos
{
    public class ReportDto
    {
        public string UserName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int TypeCount { get; set; }
    }
}

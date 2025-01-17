using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catch_up_mobile.Dtos 
{
    public class CompanySettingsDto
    {
        public int Id { get; set; }
        public Dictionary<string, bool> Settings { get; set; } = new Dictionary<string, bool>();
    }
}

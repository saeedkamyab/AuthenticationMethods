using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Blazor.Client.Models
{
    public class ClaimModel
    {
            public string Type { get; set; }
            public string Value { get; set; }
            public string Issuer { get; set; }
            // دیگر ویژگی‌های مربوط به ادعاها
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catch_up_mobile.Dtos
{
    public class ApiResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
        
    }

}

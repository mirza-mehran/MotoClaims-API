using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotoClaimsAPI.Models
{
    public class ExceptionError<T> 
    {
        public string timestamp { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public string detail { get; set; }
        public T data { get; set; }
    }

}
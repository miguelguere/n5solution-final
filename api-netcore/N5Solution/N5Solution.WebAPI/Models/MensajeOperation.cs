using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N5Solution.WebAPI.Models
{
    public class MensajeOperation
    {
        public string Id { get; set; }
        public string NameOperation { get; set; }
    }

    public static class Operations
    {
        public static string Get = "get";
        public static string Request = "request";
        public static string Modify = "modify";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientServicing.Main.Models.Debicheck
{
    public class CheckStatusResponse

    {
        public bool succeeded { get; set; }
        public string message { get; set; }
        public string? error { get; set; }
        public List  <CheckStatusRequest> result { get; set; }
    }
}

using ClientServicing.Main.Models.General;
using System.Collections.Generic;

namespace ClientServicing.Main.Models.Debicheck
{
    public class MandatesRequestResponse
    {
        public bool success { get; set; }
        public bool? didError { get; set; }
        public List<MandatesRequestResponseResult>? result { get; set; }
    }
    public class MandatesRequestResponseResult {
        public bool success { get; set; }
        public bool didError { get; set; }
        public string message { get; set; }
        public string? data { get; set; } //is return as null we need to find the data type of the property
    }
}
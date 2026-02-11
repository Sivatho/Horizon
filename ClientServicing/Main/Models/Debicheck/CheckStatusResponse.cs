using System.Collections.Generic;

namespace ClientServicing.Main.Models.Debicheck
{
    public class CheckStatusResponse
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public string? error { get; set; }
        public List<CheckStatusRequest>? result { get; set; }
    }
}
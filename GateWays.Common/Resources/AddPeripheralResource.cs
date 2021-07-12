using System;
using System.ComponentModel.DataAnnotations;

namespace GateWays.Common.Resources
{
    public class AddPeripheralResource
    {
        [Required]
        public int UID { get; set; }

        [Required]
        public string Vendor { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public long GatewayId { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace GateWays.Common.Resources
{
    public class UpdatePeripheralResource
    {
        [Required]
        public long Id { get; set; }

        public int UID { get; set; }

        public string Vendor { get; set; }

        public string Status { get; set; }

        [Required]
        public long GatewayId { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace GateWays.Common.Resources
{
    public class UpdateGatewayResource
    {
        [Required]
        public long Id { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string IpAddress { get; set; }
    }
}

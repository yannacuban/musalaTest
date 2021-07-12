using System.ComponentModel.DataAnnotations;

namespace GateWays.Common.Resources
{
    public class AddGatewayResource
    {
        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string IpAddress { get; set; }

    }
}

using System;
namespace GateWays.Common.QueryResults
{
    public class GatewayDTO
    {
        public long Id { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string IpAddress { get; set; }

        public PeripheralDTO[] PeripheralList { get; set; }
    }
}

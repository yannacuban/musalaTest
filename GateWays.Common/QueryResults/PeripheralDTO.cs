using System;
namespace GateWays.Common.QueryResults
{
    public class PeripheralDTO
    {
        public long Id { get; set; }

        public int UID { get; set; }

        public string Vendor { get; set; }

        public string Created { get; set; }

        public string Status { get; set; }

        public long GatewayId { get; set; }
    }
}

using System;
namespace GateWays.EntityFrameworkCore.Models
{
    public class Peripheral
    {
        public long Id { get; set; }

        public int UID { get; set; }

        public string Vendor { get; set; }

        public DateTime Created { get; set; }

        public string Status { get; set; }

        public long GatewayId { get; set; }
        public Gateway Gateway { get; set; }
    }
}
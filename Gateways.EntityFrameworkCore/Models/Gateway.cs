using System.Collections.Generic;

namespace GateWays.EntityFrameworkCore.Models
{
    public class Gateway
    {
        public Gateway()
        {
            PeripheralList = new HashSet<Peripheral>();
        }

        public long Id { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string IpAddress { get; set; }

        public ICollection<Peripheral> PeripheralList { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using GateWays.Common.Comunication;

namespace GateWays.Common.Utils
{
    public static class Utils
    {
        public static bool ValidateIpAddress(string ipAddress)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ipAddress))
                {
                    var splitValues = ipAddress.Split('.');
                    if (splitValues.Length != 4) return false;
                    var flag = IPAddress.TryParse(ipAddress, out IPAddress ip);
                    return flag;

                }
            }
            catch (Exception) { }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GateWays.Common.Comunication
{
    [DataContract]
    public class ErrorDataContract
    {
        public bool Success => false;
        public List<string> Messages { get; private set; }

        public ErrorDataContract(List<string> messages)
        {
            Messages = messages ?? new List<string>();
        }

        public ErrorDataContract(string message)
        {
            Messages = new List<string>();

            if (!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
            }
        }
        public ErrorDataContract()
        {
            Messages = new List<string>();
        }
    }
}

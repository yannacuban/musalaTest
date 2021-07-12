using System;
using System.Collections.Generic;
using System.Text;

namespace GateWays.Common.Comunication
{
    public class BaseResponse<T>
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public T Resource { get; private set; }

        public BaseResponse(bool success, string message, T resource)
        {
            Success = success;
            Message = message;
            Resource = resource;
        }

        public BaseResponse(T resource)
        {
            Success = true;
            Message = string.Empty;
            Resource = resource;
        }

        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
            Resource = default;
        }
    }
}

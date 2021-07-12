using System;
using System.Collections.Generic;

namespace GateWays.Common.Comunication
{
    public class PagedResponse<T>
    {
        public PagedResponse(IEnumerable<T> resource, int pageSize, int totalRecords)
        {
            Success = true;
            Message = string.Empty;
            Resource = resource;
            TotalNumberOfPages = (int)Math.Ceiling(((decimal)totalRecords / pageSize));
            TotalNumberOfRecords = totalRecords;
        }

        public PagedResponse(string message)
        {
            Success = false;
            Message = message;
            Resource = default;
        }

        /// <summary>
        /// Indicates whether the response is a success or not
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Error message in case the response is a failure
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Items
        /// </summary>
        public IEnumerable<T> Resource { get; private set; }

        /// <summary> 
        /// The total number of pages available. 
        /// </summary> 
        public int TotalNumberOfPages { get; set; }

        /// <summary> 
        /// The total number of records available. 
        /// </summary> 
        public int TotalNumberOfRecords { get; set; }
    }
}

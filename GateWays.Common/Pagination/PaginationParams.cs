using System;
using System.Runtime.Serialization;

namespace GateWays.Common.Pagination
{
    [DataContract]
    public class PaginationParams
    {
        public PaginationParams()
        {
            PageSize = 10;
            PageNumber = 1;
            Order = ESortOrder.Descending;
        }

        /// <summary>
        /// Indicates the number of items for a page
        /// </summary>
        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// Indicates the page number
        /// </summary>
        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }

        /// <summary>
        /// Indicates the order to display the items in the page
        /// </summary>
        [DataMember(Name = "order")]
        public ESortOrder Order { get; set; }
    }
}

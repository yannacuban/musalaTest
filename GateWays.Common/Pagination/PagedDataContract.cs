using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GateWays.Common.Pagination
{
    [DataContract]
    public class PagedDataContract<T> where T : class
    {
        /// <summary>
        /// Items
        /// </summary>
        [DataMember(Name = "items", Order = 3)]
        public IEnumerable<T> Items { get; private set; }

        /// <summary> 
        /// The total number of pages available. 
        /// </summary> 
        [DataMember(Name = "numberOfPages", Order = 1)]
        public int TotalNumberOfPages { get; set; }

        /// <summary> 
        /// The total number of records available. 
        /// </summary> 
        [DataMember(Name = "numberOfRecords", Order = 2)]
        public int TotalNumberOfRecords { get; set; }
    }
}

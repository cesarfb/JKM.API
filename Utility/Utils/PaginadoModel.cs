using System.Collections.Generic;

namespace JKM.UTILITY.Utils
{
    public class PaginadoResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public decimal TotalRows { get; set; }
    }

    public class DataOnlyResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
    }
}

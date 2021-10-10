using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace JKM.UTILITY.Utils
{
    public class PaginadoModel
    {
        public decimal Pages { get; set; }
        public decimal Rows { get; set; }
    }

    public class PaginadoResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public decimal TotalRows { get; set; }
        public decimal TotalPages { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeToSearch.Domain.Dtos.Responses
{
    public class GenericResponse<T>
    {
        public T? Content { get; set; }
        public string? Msg { get; set; }
        public int StatusCode { get; set; }
    }
}

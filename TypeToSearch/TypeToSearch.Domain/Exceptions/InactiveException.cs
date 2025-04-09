using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeToSearch.Domain.Exceptions
{
    public class InactiveException : Exception
    {
        public InactiveException(string message) : base(message) { }

        public InactiveException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nSt.NxtControlLib.Exceptions
{
    public class NxtControlException : Exception
    {
        public NxtControlException(string partName, string customMessage, Exception innerException)
            : base("In " + partName + ": " + customMessage + (string.IsNullOrWhiteSpace(innerException.Message) ?
                    Environment.NewLine + innerException.Message : string.Empty), innerException)
        { }


        public NxtControlException(string partName, string customMessage)
            : this(partName, customMessage, new Exception())
        { }
    }
}

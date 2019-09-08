using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.MXNet.Core
{
    public class NativeMethodException : Exception
    {
        public int ResultCode { get; private set; }

        public NativeMethodException(int resultCode, string message)
            : base(message)
        {
            ResultCode = resultCode;
        }
    }
}

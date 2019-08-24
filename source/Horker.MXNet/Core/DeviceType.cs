using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.MXNet.Core
{
    public enum DeviceType
    {
        Cpu = 1,
        Gpu = 2,
        CpuPinned = 3,
        CpuShared = 5
    }
}

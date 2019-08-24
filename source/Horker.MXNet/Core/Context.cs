using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horker.MXNet.Core
{
    /// <summary>
    /// The is equivalent to Python API's Context class defined in python/mxnet/context.py.
    /// </summary>
    public class Context
    {
        public static Context DefaultContext { get; set; } = Cpu();

        private readonly DeviceType _deviceType;
        private readonly int _deviceId;

        public DeviceType DeviceType => _deviceType;
        public int DeviceId => _deviceId;

        public Context(DeviceType deviceType, int deviceId)
        {
            _deviceType = deviceType;
            _deviceId = deviceId;
        }

        public static Context Cpu(int deviceId = 0)
        {
            return new Context(DeviceType.Cpu, deviceId);
        }

        public static Context Gpu(int deviceId = 0)
        {
            return new Context(DeviceType.Gpu, deviceId);
        }
        
        public override string ToString()
        {
            if (_deviceType == DeviceType.Gpu)
                return "gpu(" + _deviceId + ")";
            else if (_deviceType == DeviceType.Cpu)
                return "cpu(" + _deviceId + ")";
            else
                throw new NotImplementedException();
        }

        public static implicit operator string(Context ctx)
        {
            return ctx.ToString();
        }

        // Equality functions

        public override int GetHashCode()
        {
            unchecked
            {
                return (int)_deviceType * 17 + _deviceId * 397;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;

            return obj is Context d && this == d;
        }

        public static bool operator ==(Context lhs, Context rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;

            var lhsIsNull = ReferenceEquals(lhs, null);
            var rhsIsNull = ReferenceEquals(rhs, null);

            if (lhsIsNull && rhsIsNull)
                return true;

            if (lhsIsNull && !rhsIsNull || !lhsIsNull && rhsIsNull)
                return false;

            return lhs._deviceType == rhs._deviceType && lhs._deviceId == rhs._deviceId;
        }

        public static bool operator !=(Context lhs, Context rhs)
        {
            return !(lhs == rhs);
        }
    }
}

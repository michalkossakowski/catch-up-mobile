using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catch_up_mobile.Components
{
    public interface ILightSensorService
    {
        event Action<float> LightLevelChanged;
        void Start();
        void Stop();
    }
}

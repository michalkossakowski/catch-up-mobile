using Android.Content;
using Android.Hardware;
using catch_up_mobile.Components;
using Microsoft.Maui.Controls;

namespace catch_up_mobile
{
    public class LightSensorService : Java.Lang.Object, ILightSensorService, ISensorEventListener
    {
        private readonly SensorManager _sensorManager;
        private readonly Sensor? _lightSensor;
        public event Action<float>? LightLevelChanged;

        public LightSensorService()
        {
            _sensorManager = (SensorManager)MainActivity.Instance.GetSystemService(Context.SensorService);
            _lightSensor = _sensorManager.GetDefaultSensor(SensorType.Light);
            if (_lightSensor == null)
            {
                Console.WriteLine("Device does not have a light sensor. LightSensorService will operate in fallback mode.");
            }
        }

        public void Start()
        {
            if (_lightSensor != null)
            {
                _sensorManager.RegisterListener(this, _lightSensor, SensorDelay.Normal);
            }
            else
            {
                Console.WriteLine("Light sensor is not available. Start operation skipped.");
            }
        }

        public void Stop()
        {
            if (_lightSensor != null)
            {
                _sensorManager.UnregisterListener(this, _lightSensor);
            }
        }

        public void OnSensorChanged(SensorEvent? e)
        {
            if (e?.Sensor?.Type == SensorType.Light)
            {
                LightLevelChanged?.Invoke(e.Values[0]); // Passes the light intensity level
            }
        }

        public void OnAccuracyChanged(Sensor? sensor, SensorStatus accuracy)
        {
            // Handle changes in sensor accuracy here if necessary
        }
    }
}

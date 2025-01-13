using Android.Content;
using Android.Hardware;
using catch_up_mobile.Components;
using Microsoft.Maui.Controls;

namespace catch_up_mobile
{
    
    public class LightSensorService : Java.Lang.Object, ILightSensorService, ISensorEventListener
    {
        private readonly SensorManager _sensorManager;
        private readonly Sensor _lightSensor;
        public event Action<float>? LightLevelChanged;

        public LightSensorService()
        {
            _sensorManager = (SensorManager)MainActivity.Instance.GetSystemService(Context.SensorService);
            _lightSensor = _sensorManager.GetDefaultSensor(SensorType.Light);
            if (_lightSensor == null)
            {
                throw new InvalidOperationException("Device does not have a light sensor.");
            }
        }

        public void Start()
        {
            _sensorManager.RegisterListener(this, _lightSensor, SensorDelay.Normal);
        }

        public void Stop()
        {
            _sensorManager.UnregisterListener(this, _lightSensor);
        }

        public void OnSensorChanged(SensorEvent? e)
        {
            if (e?.Sensor?.Type == SensorType.Light)
            {
                LightLevelChanged?.Invoke(e.Values[0]); // Przekazuje poziom natężenia światła
            }
        }

        public void OnAccuracyChanged(Sensor? sensor, SensorStatus accuracy)
        {
            // Możesz obsłużyć zmianę dokładności sensora tutaj
        }
    }
}

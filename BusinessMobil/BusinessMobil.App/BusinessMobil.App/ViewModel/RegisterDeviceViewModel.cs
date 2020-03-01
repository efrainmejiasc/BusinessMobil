using System;
using Plugin.DeviceInfo;

namespace BusinessMobil.App.ViewModel
{
    public class RegisterDeviceViewModel
    {
        public RegisterDeviceViewModel()
        {
            DeviceId = CrossDevice.Device.DeviceId;
        }

        public string DeviceId { get; set; }
    }
}

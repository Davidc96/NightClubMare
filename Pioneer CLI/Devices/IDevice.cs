using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pioneer_CLI.Devices
{
    public interface IDevice
    {
        void PrintDeviceInfo();
        string GetDeviceName();
        int GetChannelID();

        int GetTimeOut();
        void SetTimeOut(int timeout);
        string GetIPAddress();
        string GetMacAddress();
    }
}

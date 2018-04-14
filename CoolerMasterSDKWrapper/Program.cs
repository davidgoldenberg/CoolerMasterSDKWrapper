using System;
using System.Threading;
using c = CoolerMasterSDKWrapper.CoolerMasterExported;

namespace CoolerMasterSDKWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            const string not = "not ";
            var keyboard = DEVICE_INDEX.DEV_MK750;
            c.SetControlDevice(keyboard);
            if (!c.IsDevicePlug()) {
                Console.WriteLine("Count not connect to the keyboard");
                return;
            }

            if (!c.EnableLedControl(true, keyboard))
            {
                Console.WriteLine("Count not secure LED control for the keyboard");
                return;
            }

            c.SetFullLedColor(0, 255, 0);

            Thread.Sleep(1000);

            c.EnableLedControl(false, keyboard);
        }
    }
}
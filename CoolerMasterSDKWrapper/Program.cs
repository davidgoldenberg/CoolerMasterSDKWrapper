using System;
using System.Threading;
using c = CoolerMasterSDKWrapper.CoolerMasterExported;

namespace CoolerMasterSDKWrapper
{
    static class Program
    {
        public static void SpellItOut(this string value, Action<char> action)
        {
            foreach (var c in value)
                action(c);
        }

        static void Main(string[] args)
        {
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
            
            c.SetFullLedColor(0, 0, 0);
            var matrix = new COLOR_MATRIX();
            matrix.KeyColor = new KEY_COLOR[COLOR_MATRIX.MAX_LED_ROW, COLOR_MATRIX.MAX_LED_COLUMN];
            var white = new KEY_COLOR(250,250,250);
            matrix.KeyColor[3,2] = new KEY_COLOR(250, 250, 250);
            matrix.KeyColor[2, 4] = new KEY_COLOR(250, 250, 250);
            matrix.KeyColor[4, 5] = new KEY_COLOR(250, 250, 250);
            matrix.KeyColor[5, 5] = new KEY_COLOR(250, 250, 250);
            matrix.KeyColor[3, 4] = new KEY_COLOR(250, 250, 250);
            c.SetAllLedColor(matrix, keyboard);
            // Rolling();
            c.EnableLedControl(false, keyboard);
        }

        public static void Rolling()
        {
            for (byte x = 0; x < 8; x++)
                for (byte i = 0; i < 26; i++)
                    Blink(x, i, 100);
        }

        public static void Blink(byte row, byte column, int ms)
        {
            Thread.Sleep(ms);
            c.SetLedColor(row, column, 250, 250, 250);
            Thread.Sleep(ms);
            c.SetLedColor(row, column, 0, 0, 0);
        }

        public static void Spell()
        {
            var ms = 250;
            "zxcvbnm,./".SpellItOut(x => {
                var key = Key.Keys[x];
                Blink(key.Row, key.Column, ms);
            });
        }
    }
}
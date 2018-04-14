using System.Runtime.InteropServices;

namespace CoolerMasterSDKWrapper
{
    /*
US Layout, MasterKeys Pro L(MK750), Row or Column with 7 * 24 / by each																								
Index	0	        1	        2	    3	    4	    5	    6	    7	    8	    9	    10      11	    12	    13	    14	        15	        16	            17	        18	    19	    20	    21	    22	        23
0	    ESC	        F1	        F2	    F3	    F4	 	        F5	    F6	    F7	    F8	 	        F9	    F10	    F11	    F12	        Snapshot    "ScrollLock"	Pause	    P1	    P2	    P3	    P4	    EX_LED 1	EX_LED 23
1	    `	        1	        2	    3	    4	    5	    6	    7	    8	    9	    0	    -	    =		        Backspace	Insert	    Home	        PageUp	    Numlock	(/)	    (*)	    (-)	    EX_LED 2	EX_LED 24
2	    Tab	        Q	        W	    E	    R	    T	    Y	    U	    I	    O	    P	    [	    ]		        \	        Delete	    End	            PageDown	(7)	    (8)	    (9)	    (+)	    EX_LED 3	EX_LED 25
3	    CapsLock	A	        S	D	F	G	H	J	K	L	;	'			Enter				(4)	(5)	(6)		EX_LED 4	EX_LED 26
4	    LShift		            Z	X	C	V	B	N	M	,	.	/			Rshift		Up		(1)	(2)	(3)	(Enter)		
5	    LCtrl	    LWin	    LAlt				            Space				            RAlt	RWin	App		        RCtrl	    Left	    Down	        Right	    (0)		        (.)			
6	    EX_LED 5	EX_LED 6	EX_LED 7	EX_LED 8	EX_LED 9	EX_LED 10	EX_LED 11	EX_LED 12	EX_LED 13	EX_LED 14	EX_LED 15	EX_LED 16	EX_LED 17	EX_LED 18	EX_LED 19	EX_LED 20	EX_LED 21	EX_LED 22						
*/
    public struct KEY_COLOR
    {
        /// <summary>
        /// Red
        /// </summary>
        public byte r;
        /// <summary>
        /// Green
        /// </summary>
        public byte g;
        /// <summary>
        /// Blue
        /// </summary>
        public byte b;
        public KEY_COLOR(byte _r, byte _g, byte _b)
        {
            r = _r;
            g = _g;
            b = _b;
        }
    };

    //  set up/save the whole LED color structure
    public struct COLOR_MATRIX
    {
        public const int MAX_LED_ROW = 7;
        public const int MAX_LED_COLUMN = 24;
        public KEY_COLOR[][] KeyColor;
    }

    /// <summary>
    /// 1. System Information: for the user's computer to fetch the local system time, CPU usage, memory usage percentage, the current playback volume percentage.
    /// 2. Select Device: to select the device that you want to control, the default option is MasterKeys Pro L.
    /// 3. LED Control: can choose from enable and disable, in the disable state can switch effects; in the enable state can setup the keyboard LED color
    /// 4. Set LED Color for every Key: is allowed to set different colors of each key, there are two ways to set up.One is to set a single Key; the other one is to set all keys on the keyboard to specified / different color.
    /// 5. Set All Led: set the whole keyboard as a single color quickly.
    /// 6. Set the Key effect : if it enable and the button status change, the Led of key will light.
    /// </summary>
    public static class CoolerMasterExported
    {
        // typedef void (CALLBACK* KEY_CALLBACK) (int iRow, int iColumn, bool bPressed);
        public delegate void KEY_CALLBACK(int iRow, int iColumn, bool bPressed);

        /// <summary>
        /// Get SDK Dll's Version
        /// </summary>
        /// <returns>DLL's Version</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern int GetCM_SDK_DllVer();

        /// <summary>
        /// Obtain current system time
        /// </summary>
        /// <returns>format is %Y %m/%d %H:%M %S</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Auto)]
        public static extern string GetNowTime();

        /// <summary>
        /// obtain current CPU usuage ratio
        /// </summary>
        /// <param name="pErrorCode"></param>
        /// <returns>0-100</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern long GetNowCPUUsage(out uint pErrorCode);

        /// <summary>
        /// Obtain current RAM usuage ratio
        /// </summary>
        /// <returns>0-100</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern uint GetRamUsage();

        /// <summary>
        /// Obtain current volume
        /// </summary>
        /// <returns>0-1 floating</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern float GetNowVolumePeekValue();

        /// <summary>
        /// set operating device
        /// </summary>
        /// <param name="devIndex">device list DEV_MKeys_L, DEV_MKeys_S, DEV_MOUSE one among three(currently no mouse)</param>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern void SetControlDevice(DEVICE_INDEX devIndex);

        /// <summary>
        /// verify if the deviced is plugged in
        /// </summary>
        /// <param name="devIndex"></param>
        /// <returns>true plugged in</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern bool IsDevicePlug(DEVICE_INDEX devIndex = DEVICE_INDEX.DEV_DEFAULT);

        /// <summary>
        /// Obtain current device layout
        /// </summary>
        /// <param name="devIndex"></param>
        /// <returns>LAYOUT_UNINIT || LAYOUT_US || LAYOUT_EU</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern LAYOUT_KEYBOARD GetDeviceLayout(DEVICE_INDEX devIndex = DEVICE_INDEX.DEV_DEFAULT);

        /// <summary>
        /// set control over device’s LED
        /// </summary>
        /// <param name="bEnable">true Controlled by SW，false Controlled by FW</param>
        /// <param name="devIndex"></param>
        /// <returns>true Success</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern bool EnableLedControl(bool bEnable, DEVICE_INDEX devIndex = DEVICE_INDEX.DEV_DEFAULT);

        /// <summary>
        /// switch device current effect
        /// </summary>
        /// <param name="iEffectIndex">index value of the effect</param>
        /// <param name="devIndex"></param>
        /// <returns>true for success</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern bool SwitchLedEffect(EFF_INDEX iEffectIndex, DEVICE_INDEX devIndex = DEVICE_INDEX.DEV_DEFAULT);

        /// <summary>
        /// Print out the lights setting from Buffer to LED
        /// </summary>
        /// <param name="bAuto">false means manual, call this function once, then print out once. true means auto, any light update will print out directly</param>
        /// <param name="devIndex"></param>
        /// <returns>true for success</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern bool RefreshLed(bool bAuto = false, DEVICE_INDEX devIndex = DEVICE_INDEX.DEV_DEFAULT);

        /// <summary>
        /// set entire keyboard LED color
        /// </summary>
        /// <param name="r">red</param>
        /// <param name="g">green</param>
        /// <param name="b">blue</param>
        /// <param name="devIndex"></param>
        /// <returns>true for success</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool SetFullLedColor(byte r, byte g, byte b, DEVICE_INDEX devIndex = DEVICE_INDEX.DEV_DEFAULT);

        /// <summary>
        /// Set Keyboard "every LED" color
        /// </summary>
        /// <param name="colorMatrix">fill up RGB value according to LED Table</param>
        /// <param name="devIndex"></param>
        /// <returns>true for success</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern bool SetAllLedColor(COLOR_MATRIX colorMatrix, DEVICE_INDEX devIndex = DEVICE_INDEX.DEV_DEFAULT);

        /// <summary>
        /// Set single Key LED color
        /// </summary>
        /// <param name="iRow"></param>
        /// <param name="iColumn"></param>
        /// <param name="r">red</param>
        /// <param name="g">green</param>
        /// <param name="b">blue</param>
        /// <returns>true for success</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern bool SetLedColor(int iRow, int iColumn, byte r, byte g, byte b, DEVICE_INDEX devIndex = DEVICE_INDEX.DEV_DEFAULT);

        /// <summary>
        /// To enable the call back function. will call the call back function of <see cref="SetKeyCallBack(KEY_CALLBACK, DEVICE_INDEX)"/>
        /// </summary>
        /// <param name="bEnable"></param>
        /// <param name="devIndex"></param>
        /// <returns>true for success</returns>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern bool EnableKeyInterrupt(bool bEnable, DEVICE_INDEX devIndex = DEVICE_INDEX.DEV_DEFAULT);

        /// <summary>
        /// Setup the call back function of button
        /// </summary>
        [DllImport("SDKDLL.dll", CharSet = CharSet.Unicode)]
        public static extern void SetKeyCallBack(KEY_CALLBACK callback, DEVICE_INDEX devIndex = DEVICE_INDEX.DEV_DEFAULT);
    }
}
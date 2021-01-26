using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssaultCube_trainer_v1
{
    class Offsets
    {
        public static string ProcessName = "ac_client.exe";
        public static string BaseAddress = "509B74";

        public static string DualPistolOffset = "10C";
        public static string Crosshair = "0x50F20C";
        public static string Recoil = "0x4EE444";

        public static string ARammo = "00109B74,150";
        public static string SMGammo = "00109B74,148";
        public static string SRammo = "00109B74,14C";
        public static string SGammo = "00109B74,144";
        public static string DPammo = "509B74,15C";

        public static string Health = "00109B74,F8";
        public static string Armour = "0010F4F4,FC";
        public static string Grenades = "00109B74,158";


        public static string FragKills = "00109B74,1FC";
        public static string Deaths = "00109B74,204";
    }
}

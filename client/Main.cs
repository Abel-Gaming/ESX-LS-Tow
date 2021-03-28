using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace client
{
    public class Main : BaseScript
    {
        public static string Author = "Abel Gaming";
        public static string ModName = "ESX LS Towing";
        public static string Version = "1.0";

        public static bool OnDuty = false;

        public Main()
        {
            API.RegisterCommand("towmenu", new Action(TowMenu), false);
        }

        private static void TowMenu()
        {
            if (!Menu.mainMenu.Visible)
            {
                Menu.mainMenu.Visible = true;
            }
            else
            {
                Menu.mainMenu.Visible = false;
            }
        }
    }
}
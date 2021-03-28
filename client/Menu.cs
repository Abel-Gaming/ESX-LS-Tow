using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

using NativeUI;

namespace client
{
    public class Menu : BaseScript
    {
        public static MenuPool _menuPool;
        public static UIMenu mainMenu;

        public static Vector3 VehicleSpawn = new Vector3(392.84f, -1618.54f, 28.79f);
        public static float VehicleSpawnHeading = 320.49f;

        private static void MainMenuOptions(UIMenu menu)
        {
            var ToggleDuty = new UIMenuItem("Toggle Duty");
            mainMenu.AddItem(ToggleDuty);
            mainMenu.OnItemSelect += (sender, item, index) =>
            {
                if (item == ToggleDuty)
                {
                    if (!Main.OnDuty)
                    {
                        TriggerEvent("swt_notifications:captionIcon", "You are now on duty as a tow truck driver!", "On Duty", "top-right", 2500, "green-10", "white", true, "mdi-car-pickup");
                        Main.OnDuty = true;
                        TriggerServerEvent("NJRP:TowTruckDriverOnDuty");
                    }
                    else
                    {
                        TriggerEvent("swt_notifications:captionIcon", "You are now off duty as a tow truck driver!", "Off Duty", "top-right", 2500, "yellow-10", "black", true, "mdi-car-pickup");
                        Main.OnDuty = false;
                        TriggerServerEvent("NJRP:TowTruckDriverOffDuty");
                    }
                }
            };
        }

        private static void VehicleMenu(UIMenu menu)
        {
            var vehiclesub = _menuPool.AddSubMenu(menu, "Vehicles");
            for (int i = 0; i < 1; i++) ;

            vehiclesub.MouseEdgeEnabled = false;
            vehiclesub.ControlDisablingEnabled = false;

            var scflatbed = new UIMenuItem("Single Cab Flatbed", "You must pay the lease amount to use this vehicle.");
            scflatbed.SetRightLabel("~g~$5,000");
            vehiclesub.AddItem(scflatbed);
            vehiclesub.OnItemSelect += async (sender, item, index) =>
            {
                if (item == scflatbed)
                {
                    TriggerServerEvent("NJRP:RemoveMoney", 5000);
                    TriggerEvent("swt_notifications:Icon", "Lease Paid", "top-right", 2500, "red-10", "white", true, "mdi-currency-usd");
                    var modelName = "f550rb";
                    var modelHash = (uint)API.GetHashKey(modelName);
                    if (API.IsModelInCdimage(modelHash))
                    {
                        API.RequestModel(modelHash);
                        while (!API.HasModelLoaded(modelHash))
                        {
                            await Delay(0);
                        }
                        if (Game.Player.Character.IsInVehicle())
                        {
                            Game.Player.Character.CurrentVehicle.Delete();
                        }
                        var vehicle = API.CreateVehicle(modelHash, VehicleSpawn.X, VehicleSpawn.Y, VehicleSpawn.Z, VehicleSpawnHeading, true, true);
                        API.TaskWarpPedIntoVehicle(API.GetPlayerPed(-1), vehicle, -1);
                    }
                    else
                    {
                        Screen.ShowNotification("~r~Model does not exist");
                    }
                }
            };

            var flatbed = new UIMenuItem("Flatbed", "You must pay the lease amount to use this vehicle.");
            flatbed.SetRightLabel("~g~$5,000");
            vehiclesub.AddItem(flatbed);
            vehiclesub.OnItemSelect += async (sender, item, index) =>
            {
                if (item == flatbed)
                {
                    TriggerServerEvent("NJRP:RemoveMoney", 5000);
                    TriggerEvent("swt_notifications:Icon", "Lease Paid", "top-right", 2500, "red-10", "white", true, "mdi-currency-usd");
                    var modelName = "flatbed";
                    var modelHash = (uint)API.GetHashKey(modelName);
                    if (API.IsModelInCdimage(modelHash))
                    {
                        API.RequestModel(modelHash);
                        while (!API.HasModelLoaded(modelHash))
                        {
                            await Delay(0);
                        }
                        if (Game.Player.Character.IsInVehicle())
                        {
                            Game.Player.Character.CurrentVehicle.Delete();
                        }
                        var vehicle = API.CreateVehicle(modelHash, VehicleSpawn.X, VehicleSpawn.Y, VehicleSpawn.Z, VehicleSpawnHeading, true, true);
                        API.TaskWarpPedIntoVehicle(API.GetPlayerPed(-1), vehicle, -1);
                    }
                    else
                    {
                        Screen.ShowNotification("~r~Model does not exist");
                    }
                }
            };
        }

        public Menu()
        {
            _menuPool = new MenuPool();
            mainMenu = new UIMenu("Tow Service", "~b~Mod by ~b~Abel Gaming");
            _menuPool.Add(mainMenu);

            MainMenuOptions(mainMenu);
            VehicleMenu(mainMenu);

            _menuPool.MouseEdgeEnabled = false;
            _menuPool.ControlDisablingEnabled = false;
            _menuPool.RefreshIndex();

            Tick += async () =>
            {
                _menuPool.ProcessMenus();
            };
        }
    }
}

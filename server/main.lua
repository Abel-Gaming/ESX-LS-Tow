ESX = nil

TriggerEvent('esx:getSharedObject', function(obj) ESX = obj end)

RegisterNetEvent("NJRP:GiveMoney", amount)
AddEventHandler("NJRP:GiveMoney", function(amount)
        local xPlayer = ESX.GetPlayerFromId(source)
        xPlayer.addMoney(amount)
end)

RegisterNetEvent("NJRP:RemoveMoney", amount)
AddEventHandler("NJRP:RemoveMoney", function(amount)
        local xPlayer = ESX.GetPlayerFromId(source)
        xPlayer.removeMoney(amount)
end)

RegisterNetEvent("NJRP:TowTruckDriverOnDuty")
AddEventHandler("NJRP:TowTruckDriverOnDuty", function()
        local xPlayer = ESX.GetPlayerFromId(source)
        xPlayer.setJob("mechanic", 1)
        TriggerClientEvent("swt_notifications:Icon", source, xPlayer.getName() .. " is now on duty as a tow truck driver. Please notify in chat if you need a tow!", "top-right", 4500, "blue-10", "white", true, "mdi-tow-truck")
end)

RegisterNetEvent("NJRP:TowTruckDriverOffDuty")
AddEventHandler("NJRP:TowTruckDriverOffDuty", function()
        local xPlayer = ESX.GetPlayerFromId(source)
        xPlayer.setJob("unemployed", 0)
        TriggerClientEvent("swt_notifications:Icon", source, xPlayer.getName() .. " is now off duty as a tow truck driver. Please do not contact this person for a tow.", "top-right", 4500, "blue-10", "white", true, "mdi-tow-truck")
end)
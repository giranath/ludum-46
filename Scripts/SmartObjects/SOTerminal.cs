using Godot;
using System;

public class SOTerminal : SmartObject
{
    public void ShowTime(Item item)
    {
        GD.Print("wow refuel");
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        itemActionMap.Add(itemType.PowerCell, ShowTime);
        itemActionMap.Add(itemType.FireExtinguisher, ShowTime);
        itemActionMap.Add(itemType.Pistol, ShowTime);
    }

}

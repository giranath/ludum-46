using Godot;
using System;

public class FuelCell : Item { 

    FuelCell() {
        Type = itemType.PowerCell;
    }

    public override void act() {
        GD.Print("Use Fuel");
    }
}

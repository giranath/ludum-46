using Godot;
using System;

public class Gun : Item { 

    public override void act() {
        GD.Print("Shoot BAM BAM!");
    }
}

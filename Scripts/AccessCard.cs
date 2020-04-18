using Godot;
using System;

public class AccessCard : Item
{
    [Export]
    int key;

    public AccessCard() {
        Type = itemType.AccessCard;
    }

    public override void act() {
        
    }
}

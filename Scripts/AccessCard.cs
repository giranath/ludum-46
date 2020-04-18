using Godot;
using System;

public class AccessCard : Item
{
    [Export]
    public int key;

    public AccessCard() {
        Type = itemType.AccessCard;
    }

    public override void act() {
        
    }
}

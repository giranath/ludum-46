using Godot;
using System;


public abstract class Item : Node2D {
    public itemType type;
    public abstract void act();

    public void throwItem(Node2D Character) {
        GD.Print("Throw!");
        GetParent().RemoveChild(this);
        Character.GetParent().AddChild(this);
        Position = Character.Position;
    }

    protected void bodyEnter(object body) {
        var inventaire = ((Node)body).GetParent().FindNode("Inventaire") as Inventory;
        if (inventaire != null) {
            inventaire.groundItem = GetPath();
        }
    }

    protected void bodyExit(object body) {
        var inventaire = ((Node)body).GetParent().FindNode("Inventaire") as Inventory;
        if (inventaire != null) {
            inventaire.groundItem = null;
        }
    }
}

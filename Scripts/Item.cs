using Godot;
using System;

public enum itemType
{
	None,
	PowerCell,
	FireExtinguisher,
	Pistol,
	AccessCard,
    ToolBox,
    Syringe,
    DuckTape,
}

public abstract class Item : Node2D {

	public itemType Type {get;set;}

	public abstract void act();

    public abstract void ItemPicked();

    public abstract void ItemDropped();

    public void pickItem(Node newParent)
    {
        ItemPicked();
        GetParent().RemoveChild(this);
        newParent.AddChild(this);
        Position = Vector2.Zero;
    }

    public void throwItem(Node2D Character) {
        ItemDropped();
        GetParent().RemoveChild(this);
		Character.GetParent().AddChild(this);
		Position = Character.Position + new Vector2(0, -20);
	}

	protected void bodyEnter(object body) {
	
		var inventaire = ((Node)body).GetParent().FindNode("Inventory") as Inventory;
		if (inventaire != null) {
			inventaire.groundItem = GetPath();
		}
	}

	protected void bodyExit(object body) {
	
		var inventaire = ((Node)body).GetParent().FindNode("Inventory") as Inventory;
		if (inventaire != null) {
			inventaire.groundItem = null;
		}
	}
}

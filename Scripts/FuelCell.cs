using Godot;
using System;

public class FuelCell : Item { 

	FuelCell() {
		Type = itemType.PowerCell;
	}

	public override void act() {
		GD.Print("Use Fuel");
	}

	public override void ItemDropped()
	{
		GD.Print("Enabled");
		var item = GetNode<CollisionShape2D>("PickupArea/PickupShape");
		item.Disabled = false;
	}

	public override void ItemPicked()
	{
		GD.Print("Disabled");
		var item = GetNode<CollisionShape2D>("PickupArea/PickupShape");
		item.Disabled = true;
	}
}

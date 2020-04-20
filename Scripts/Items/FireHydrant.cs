using Godot;
using System;

public class FireHydrant : Item
{
	public FireHydrant()
	{
		Type = itemType.FireExtinguisher;
	}

	public override void act()
	{
		// Emmiter here!
		GD.Print("Use syringe");
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

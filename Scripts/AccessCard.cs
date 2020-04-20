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

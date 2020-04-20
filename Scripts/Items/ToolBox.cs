using Godot;
using System;

public class ToolBox : Item
{
	
	ToolBox()
	{
		Type = itemType.ToolBox;
	}
	
	public override void act()
	{
		GD.Print("Use toolBox");
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

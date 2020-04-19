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
}

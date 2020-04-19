using Godot;
using System;

public class Syringe : Item
{
	Syringe()
	{
		Type = itemType.Syringe;
	}

	public override void act()
	{
		GD.Print("Use syringe");
	}
}

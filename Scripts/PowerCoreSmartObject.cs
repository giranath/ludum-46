using Godot;
using System;

public class PowerCoreSmartObject : SmartObject
{
	int fuel = 10;
	
	public void refuel()
	{
		fuel += 15;
		GD.Print("wow refuel");
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		itemActionMap.Add(itemType.PowerCell, refuel);
	}
	
}

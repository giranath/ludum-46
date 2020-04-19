using Godot;
using System;

public class PowerCoreSmartObject : SmartObject, Destroyable
{
	

	public void refuel(Item item)
	{
		gameState.Fuel += 25.0f;

		GD.Print("wow refuel");
		item.QueueFree();

	}

	public void Repair(Item value)
	{
		ShowBrokenLight(false);
		itemActionMap.Clear();
		itemActionMap.Add(itemType.PowerCell, refuel);
		//item.QueueFree();
	}

	public void Destroy()
	{
		ShowBrokenLight(true);
		itemActionMap.Clear();
		itemActionMap.Add(itemType.ToolBox, Repair);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		itemActionMap.Add(itemType.PowerCell, refuel);
		gameState.destroyables.Add(this);
	}
}

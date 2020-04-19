using Godot;
using System;

public class PowerCoreSmartObject : SmartObject
{
	
	public void refuel(Item item)
	{
		GameState gamestate = GetNode<GameState>("/root/GameState");
		gamestate.fuel += 25.0f;

		GD.Print("wow refuel");
		item.QueueFree();

	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		itemActionMap.Add(itemType.PowerCell, refuel);
	}
	
}

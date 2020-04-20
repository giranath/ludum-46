using Godot;
using System;

public class PowerCoreSmartObject : SmartObject, Destroyable
{
	

	public void refuel(Item item)
	{
		if(item != null)
		{
			gameState.Fuel += 25.0f;
	
			GD.Print("wow refuel");
			item.QueueFree();
            gameState.uiManager.DialogUI.SetText(3, "You place the fuel tank and you hear crackling of the fire .", Colors.Green);
        }
		else
		{ 
			gameState.uiManager.DialogUI.SetText(3, "I wouldn't touch this too much. Maybe you need a power cell?", Colors.White);
		}
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
		itemActionMap.Add(itemType.None, refuel);
		gameState.destroyables.Add(this);
	}
}

using Godot;
using System;

public class SmartObjectTester : Node2D
{
	[Export]
	public NodePath smartObject;

	float timer = 5.0F;
	bool timerHasElapsed = false;
	Item PowerCellItem = new Item(itemType.PowerCell);
	Item PistolItem = new Item(itemType.Pistol);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PowerCellItem.type = itemType.PowerCell;
		PistolItem.type = itemType.Pistol;

	}

  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
 	{
	  	timer -= delta;
		if(timer < 0 && !timerHasElapsed)
		{
			timerHasElapsed = true;
			SmartObject leSmartObject = (SmartObject)GetNode(smartObject);
			leSmartObject.interact(PowerCellItem);
			leSmartObject.interact(PistolItem);
		}
  	}
}

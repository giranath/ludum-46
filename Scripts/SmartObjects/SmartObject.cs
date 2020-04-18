using Godot;
using System;
using System.Collections.Generic;

public class SmartObject : Node
{

	public Dictionary<itemType, Action<Item>> itemActionMap = new Dictionary<itemType, Action<Item>>();
	bool playerInside = false;

	public override void _Ready() {
		base._Ready();
		Area2D clickArea = GetNode<Area2D>("./ClickArea");

		clickArea.Connect("input_event", this, "OnClickSmartObject");
	}

	public virtual void interact(Item item)
	{
		itemType typeToTest = item != null ? item.Type : itemType.None;

		Action<Item> itemAction;
		if (itemActionMap.TryGetValue(typeToTest, out itemAction))
		{
			GD.Print("Used item:" + (int)typeToTest);
			itemAction(item);
		}
		else
		{
			GD.Print("Used an invalid item on this smart object");
		}
	}

	protected virtual void OnClickSmartObject(object viewport, object @event, int shape_idx)
	{
		if (@event is InputEventMouseButton btn && btn.ButtonIndex == (int)ButtonList.Left && btn.IsPressed() && playerInside)
		{
			interact(null);
		}
	}
	
	private void SmartObjectBodyEntered(object body)
	{
		var characterMovement = ((Node)body) as CharacterMovement;

		if (characterMovement != null)
		{
			playerInside = true;
		}
	}


	private void SmartObjectBodyExited(object body)
	{
		var characterMovement = ((Node)body) as CharacterMovement;

		if (characterMovement != null)
		{
			playerInside = false;
		}
	}
}

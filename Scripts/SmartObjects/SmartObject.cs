using Godot;
using System;
using System.Collections.Generic;

public enum itemType
{
	PowerCell,
	FireExtinguisher,
	Pistol
}

public class SmartObject : Node
{
	public Dictionary<itemType, Action> itemActionMap = new Dictionary<itemType, Action>();
	bool playerInside = false;

	public virtual void interact(Item item)
	{
		Action itemAction;
		if (itemActionMap.TryGetValue(item.type, out itemAction))
		{
			GD.Print("Used item:" + (int)item.type);
			itemAction();
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
			Item item = new Gun();
			interact(item);
			GD.Print("Clicked");
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




using Godot;
using System;
using System.Collections.Generic;

public enum itemType
{
	None,
	PowerCell,
	FireExtinguisher,
	Pistol,
	AccessCard
}

public class SmartObject : Node
{

	public Dictionary<itemType, Action<Item>> itemActionMap = new Dictionary<itemType, Action<Item>>();
	bool playerInside = false;

	public virtual void interact(Item item)
	{
		itemType typeToTest = item != null ? item.type : itemType.None;

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
			Item item = new Item(itemType.PowerCell);
			interact(null);
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




using Godot;
using System;
using System.Collections.Generic;

public class SmartObject : Node
{
	public Dictionary<itemType, Action<Item>> itemActionMap = new Dictionary<itemType, Action<Item>>();
	
	bool playerInside = false;

	public GameState gameState;

	[Export]
	NodePath brokenLightPath;

	Sprite brokenLight;

	[Export]
	float rotationSpeed = 0.5f;

	public override void _Ready() {		

		gameState = GetNode<GameState>("/root/GameState");

		if(brokenLightPath != null)
		{
			brokenLight = GetNode<Sprite>(brokenLightPath);
		}
	}

	public override void _Process(float delta)
	{
		brokenLight?.Rotate(rotationSpeed * delta);
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
	
	public virtual void OnClick()
	{
		
		if(playerInside)
		{
			interact(gameState.player.GetItemInCurrentHand());
		}
	}

	protected void ShowBrokenLight(bool show)
	{
		brokenLight.Visible = show;
	}
}




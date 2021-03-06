using Godot;
using System;

public class SOTerminal : SmartObject
{
	public UIManager uiManager = null;

	public void ShowTime(Item item)
	{
		if(gameState == null || uiManager == null)
		{
			gameState = GetNode<GameState>("/root/GameState");
			uiManager = gameState.uiManager;

			GD.Print(gameState.Name);
		}

		uiManager.DialogUI.SetText(5, String.Format("Arriving at destination in {0} light years", (int)gameState.GetDistanceToTravel()), Colors.White);
	}

	public override void _Ready()
	{
		base._Ready();
		itemActionMap.Add(itemType.None, ShowTime);
	}
}

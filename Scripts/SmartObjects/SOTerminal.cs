using Godot;
using System;

public class SOTerminal : SmartObject
{
	public UIManager uiManager = null;

	public GameState gameState = null;

	public void ShowTime(Item item)
	{
		if(gameState == null)
		{
			gameState = GetNode<GameState>("/root/GameState");
			uiManager = gameState.uiManager;

			GD.Print(gameState.Name);
		}

		uiManager.DialogUI.SetText(5, $"Only 5 minutes left you are almost there!");
	}

	public override void _Ready()
	{
		itemActionMap.Add(itemType.None, ShowTime);
	}
}

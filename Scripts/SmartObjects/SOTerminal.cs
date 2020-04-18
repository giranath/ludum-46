using Godot;
using System;

public class SOTerminal : SmartObject
{
	private TimedRepeater timed;

	private UIManager uiManager = null;

	private GameState gameState = null;

	public void ShowTime()
	{
		GD.Print("ADASDSA");
		if(uiManager == null)
		{
			uiManager = GetNode<UIManager>("/root/UIManager");
		}

		if(gameState == null)
		{
			gameState = GetNode<GameState>("/root/GameState");
		}

		uiManager.DialogUI.SetText(5, $"Only 5 minutes left you are almost there!");
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		itemActionMap.Add(itemType.PowerCell, ShowTime);
		itemActionMap.Add(itemType.FireExtinguisher, ShowTime);
		itemActionMap.Add(itemType.Pistol, ShowTime);
	}

}

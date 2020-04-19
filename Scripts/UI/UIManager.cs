using Godot;
using System;

public class UIManager : CanvasLayer
{
	[Export]
	public NodePath DialogUIPath;

	[Export]
	public NodePath ZonePromptUIPath;

	public DialogUI DialogUI;

	public DialogUI ZonePromptUI;

	public TimedRepeater bla;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DialogUI = GetNode<DialogUI>(DialogUIPath);
		ZonePromptUI = GetNode<DialogUI>(ZonePromptUIPath);
		
		var gameState = GetNode<GameState>("/root/GameState");
		gameState.uiManager = this;
	}

	public override void _Process(float delta)
	{
	}

    public void OnRoomChanged(Room newRoom)
    {
        ZonePromptUI.SetText(5.0f, newRoom.roomName);
    }
}

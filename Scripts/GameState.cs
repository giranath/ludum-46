using Godot;
using System;

public class GameState : Node
{
	public CharacterMovement player;

	public UIManager uiManager;

	public float fuel { get; set; } = 75;
	
	public Viewport viewport;
}

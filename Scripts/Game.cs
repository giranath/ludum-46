using Godot;
using System;

public class Game : Node2D
{
	public override void _Ready()
	{
		GameState gameState = GetNode<GameState>("/root/GameState");

		gameState.gameRootNode = GetPath();
	}
}

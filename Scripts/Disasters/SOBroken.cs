using Godot;
using System;

public class SOBroken : Disaster
{

	GameState gameState;
	public override bool IsAvailable()
	{
		return gameState.destroyables.Count != 0;
	}

	public override void Process()
	{
		var rand = new RandomNumberGenerator();
		int index = rand.RandiRange(0, gameState.destroyables.Count - 1);

		gameState.destroyables[index].Destroy();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameState = GetNode<GameState>("/root/GameState");
	}
}

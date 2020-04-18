using Godot;
using System;

public class FuelManager : Node
{
	private TimedRepeater TimedRepeater;

	[Export]
	public int fuelReductionCooldown = 5;

	[Export]
	public float fuelReduction = 1.0f;

	private void ReduceFuel(int count)
	{
		GameState gamestate = GetNode<GameState>("/root/GameState");
		gamestate.fuel -= fuelReduction;
		GD.Print("Fuel : " + gamestate.fuel.ToString());
	}

	public override void _Process(float delta)
	{
		TimedRepeater._Process(delta);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TimedRepeater = new TimedRepeater(fuelReductionCooldown, 0, ReduceFuel);
	}
	
}

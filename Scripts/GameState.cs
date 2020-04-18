using Godot;
using System;

public class GameState : Node
{
	public UIManager uiManager;

	public CharacterMovement player;

	public Station station;

	public float fuel { get; set; } = 75;

	public float maxFuel = 125;

	public float traveledDistance = 0.0f;

	public float requiredTravelDistance = 6000.0f;

	private bool destinationReached = false;

	[Signal]
	public delegate void ReachedDestination();

	public float GetDistanceToTravel() {
		return Mathf.Clamp(requiredTravelDistance - traveledDistance, 0.0f, requiredTravelDistance);
	}

	public override void _Process(float delta) {
		GD.Print("Traveled ", traveledDistance, " meters");

		if(traveledDistance >= requiredTravelDistance && !destinationReached) {
			EmitSignal(nameof(ReachedDestination));
			destinationReached = true;
		}
	}
}

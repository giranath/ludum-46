using Godot;
using System;
using System.Collections.Generic;

public class GameState : Node
{
	public UIManager uiManager;

	public CharacterMovement player;

	public Station station;

    private float _fuel = 75;
    public float Fuel { 
        get { return _fuel; } 
        set { this._fuel = Mathf.Clamp(value, 0.0f, maxFuel); } 
    }

	public float maxFuel = 125;

	public float traveledDistance = 0.0f;

	public float requiredTravelDistance = 6000.0f;

	private bool destinationReached = false;

    public float currentShipSpeed = 0.0f;

    public List<Destroyable> destroyables = new List<Destroyable>();

	[Signal]
	public delegate void ReachedDestination();

	public float GetDistanceToTravel() {
		return Mathf.Clamp(requiredTravelDistance - traveledDistance, 0.0f, requiredTravelDistance);
	}

	public override void _Process(float delta) {
		if(traveledDistance >= requiredTravelDistance && !destinationReached) {
			EmitSignal(nameof(ReachedDestination));
			destinationReached = true;
		}
	}

    public void OnMissionLost()
    {
        GD.Print("Missing lost");
    }
}

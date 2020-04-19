using Godot;
using System;

public class Station : Node2D
{
	[Export]
	public Curve fuelToSpeedCurve;

	[Export]
	public float maxSpeed;

	private GameState gameState;

    private RoomGraph rooms;

	public bool PropulsorBroken { get; set; } = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameState = GetNode<GameState>("/root/GameState");
		gameState.station = this;

        rooms = GetNode<RoomGraph>("./Rooms");
        rooms.Connect("PlayerChangedRoom", gameState.uiManager, "OnRoomChanged");
	}

    public RoomGraph GetRooms()
    {
        return rooms;
    }

	public float GetSpeedAtFuel(float fuel, float maxFuel) {
		return fuelToSpeedCurve.Interpolate(Mathf.Clamp(fuel / maxFuel, 0.0f, 1.0f)) * maxSpeed;
	}

	public override void _Process(float delta) {
		if(!PropulsorBroken)
		{
            gameState.currentShipSpeed = GetSpeedAtFuel(gameState.Fuel, gameState.maxFuel);
            gameState.traveledDistance += gameState.currentShipSpeed * delta;
		}
	}
}

using Godot;
using System;

public class Station : Node2D
{
    [Export]
    public Curve fuelToSpeedCurve;

    [Export]
    public float maxSpeed;

    private GameState gameState;

    public bool PropulsorBroken { get; set; } = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gameState = GetNode<GameState>("/root/GameState");
        gameState.station = this;
    }

    public float GetSpeedAtFuel(float fuel, float maxFuel) {
        return fuelToSpeedCurve.Interpolate(Mathf.Clamp(fuel / maxFuel, 0.0f, 1.0f)) * maxSpeed;
    }

    public override void _Process(float delta) {
        if(!PropulsorBroken)
        {
            gameState.traveledDistance += GetSpeedAtFuel(gameState.fuel, gameState.maxFuel) * delta;
        }
    }
}

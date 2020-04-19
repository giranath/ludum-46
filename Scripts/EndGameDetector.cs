using Godot;
using System;

public class EndGameDetector : Node2D
{
    [Export]
    public NodePath roomPath;

    [Export]
    public float oxygenThresold;

    [Export]
    public float losingDuration;

    [Signal]
    public delegate void MissionLost();

    [Signal]
    public delegate void StartLosing();
    
    [Signal]
    public delegate void StopLosing();

    private float losingTimeAcc;

    private Room room;

    private bool lost = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        room = GetNode<Room>(roomPath);

        GameState gameState = GetNode<GameState>("/root/GameState");
        Connect(nameof(MissionLost), gameState, "OnMissionLost");
    }

    public override void _Process(float delta)
    {
        if(lost)
        {
            return;
        }

        float currentRoomOxygen = 0.0f;
        room.Graph.TryGetRoomProperty(room, "oxygen", out currentRoomOxygen);

        if(currentRoomOxygen < oxygenThresold)
        {
            if(losingTimeAcc == 0.0f)
            {
                EmitSignal(nameof(StartLosing));
            }

            losingTimeAcc += delta;

            if(losingTimeAcc > losingDuration)
            {
                EmitSignal(nameof(MissionLost));
                lost = true;
            }
        }
        else if(losingTimeAcc > 0.0f)
        {
            losingTimeAcc = 0.0f;
            EmitSignal(nameof(StopLosing));
        }
    }
}

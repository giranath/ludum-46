using Godot;
using System;

public class OxygenConsumer : Node
{
    [Export]
    NodePath RoomPath;

    [Export]
    float duration = 1.0f;

    [Export]
    float consumptionRate = -1.0f;

    Room room;

    private TimedRepeater TimedRepeater;

    void ConsumeOxygen(int count)
    {
        room.Graph.TryAddPropertyOfRoom(room, "oxygen", consumptionRate);
    }
    
    public override void _Ready()
    {
        room = GetNode<Room>(RoomPath);
        TimedRepeater = new TimedRepeater(duration, 0, ConsumeOxygen);
    }


    public override void _Process(float delta)
    {
        TimedRepeater._Process(delta);
    }
}

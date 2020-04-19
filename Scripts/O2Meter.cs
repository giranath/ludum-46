using Godot;
using System;

public class O2Meter : Node2D
{
    [Export]
    public NodePath owningRoomPath;

    [Export]
    Color goodColor;

    [Export]
    Color badColor;

    private Room owningRoom;
    private ColorRect progressRect;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        owningRoom = GetNode<Room>(owningRoomPath);
        progressRect = GetNode<ColorRect>("./ProgressColor");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        float oxygenValue;
        if (!owningRoom.Graph.TryGetRoomProperty(owningRoom, "oxygen", out oxygenValue))
        {
            oxygenValue = 0.0f;
        }

        Color currentColor = badColor.LinearInterpolate(goodColor, oxygenValue / 100.0f);
        progressRect.Color = currentColor;
    }
}

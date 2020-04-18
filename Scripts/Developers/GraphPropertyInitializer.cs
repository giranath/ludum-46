using Godot;
using System;

public class GraphPropertyInitializer : Node
{
    [Export]
    NodePath graphPath;

    [Export]
    NodePath roomPath;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private float delay;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        delay = 10.0f;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        delay += delta;

        if(delay >= 10.0f)
        {
            RoomGraph graph = GetNode<RoomGraph>(graphPath);

            Room room = GetNode<Room>(roomPath);

            GD.Print("Setting influence on node");
            graph.TrySetPropertyOfRoom(room, 0, 50.0f);

            delay -= 10.0f;
        }
    }
}

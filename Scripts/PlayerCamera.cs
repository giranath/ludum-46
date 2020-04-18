using Godot;
using System;

public class PlayerCamera : Node2D
{
    [Export]
    public NodePath playerPath;

    private Node2D playerNode;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerNode = GetNode<Node2D>(playerPath);
    }

    public override void _PhysicsProcess(float delta)
    {
        GlobalPosition = playerNode.GlobalPosition;
    }
}

using Godot;
using System;

public class PlayGame : Node
{
	[Export]
	public NodePath Intro;

	[Export]
	public PackedScene ground;

	public override void _Ready()
	{
	}
	private void Pressed()
	{
		var treeRoot = GetTree().Root;
		treeRoot.RemoveChild(GetNode(Intro));

		Node newGround = ground.Instance();
		treeRoot.AddChild(newGround);
	}
}




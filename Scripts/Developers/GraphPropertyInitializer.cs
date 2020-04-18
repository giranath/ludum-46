using Godot;
using System;

public class GraphPropertyInitializer : Node
{
	[Export]
	NodePath graphPath;

	[Export]
	NodePath roomPath;

	private float delay;

	public override void _Ready()
	{
		delay = 10.0f;
	}

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

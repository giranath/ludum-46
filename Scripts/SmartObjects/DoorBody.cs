using Godot;
using System;

public class DoorBody : Node
{
	private Sprite doorClosed;
	private Sprite doorOpened;
	private CollisionShape2D collisionShape;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		
		doorClosed = GetNode<Sprite>("./DoorClosed");
		doorOpened = GetNode<Sprite>("./DoorOpened");
		collisionShape = GetNode<CollisionShape2D>("./PhysicBody/DoorShape");

		GetParent().Connect("OnStateChanged", this, "OnDoorStateChanged");
	}

	private void OnDoorStateChanged(DoorState newState)
	{
		switch(newState)
		{
			case DoorState.Opened:
				doorClosed.Visible = false;
				doorOpened.Visible = true;
				collisionShape.Disabled = true;
			break;
			case DoorState.Closed:
				doorClosed.Visible = true;
				doorOpened.Visible = false;
				collisionShape.Disabled = false;
			break;
			default:
			break;
		}
	}
}

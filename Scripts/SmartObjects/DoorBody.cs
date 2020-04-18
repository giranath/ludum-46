using Godot;
using System;

public class DoorBody : Node
{
	private Sprite sprite;
	private CollisionShape2D collisionShape;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<Sprite>("./Sprite");
		collisionShape = GetNode<CollisionShape2D>("./PhysicBody/DoorShape");

		GetParent().Connect("OnStateChanged", this, "OnDoorStateChanged");
	}

	private void OnDoorStateChanged(DoorState newState)
	{
		switch(newState)
		{
			case DoorState.Opened:
				sprite.Visible = false;
				collisionShape.Disabled = true;
			break;
			case DoorState.Closed:
				sprite.Visible = true;
				collisionShape.Disabled = false;
			break;
			default:
			break;
		}
	}
}

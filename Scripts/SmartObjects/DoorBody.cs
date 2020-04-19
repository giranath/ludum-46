using Godot;
using System;

public class DoorBody : Node
{
	private Sprite doorClosed;
	private Sprite doorOpened;
    private Sprite doorLocked;
    private CollisionShape2D collisionShape;

    private bool locked;
    private DoorState lastState;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		base._Ready();
		
		doorClosed = GetNode<Sprite>("./DoorClosed");
		doorOpened = GetNode<Sprite>("./DoorOpened");
        doorLocked = GetNode<Sprite>("./DoorLocked");
        collisionShape = GetNode<CollisionShape2D>("./PhysicBody/DoorShape");

		GetParent().Connect("OnStateChanged", this, "OnDoorStateChanged");
        GetParent().Connect("LockedStateChanged", this, "OnDoorLockedChanged");
    }

	private void OnDoorStateChanged(DoorState newState)
	{
        lastState = newState;

        switch (newState)
		{
			case DoorState.Opened:
				doorClosed.Visible = false;
				doorOpened.Visible = true;
                doorLocked.Visible = false;
                collisionShape.Disabled = true;
			break;
			case DoorState.Closed:
                if(locked)
                { 
                    doorLocked.Visible = true;
                    doorClosed.Visible = false;
                }
                else
                {
                    doorClosed.Visible = true;
                    doorLocked.Visible = false;
                }

				doorOpened.Visible = false;
				collisionShape.Disabled = false;
			break;
			default:
			break;
		}
	}

    private void OnDoorLockedChanged(bool isLocked)
    {
        locked = isLocked;
        OnDoorStateChanged(lastState);
    }
}

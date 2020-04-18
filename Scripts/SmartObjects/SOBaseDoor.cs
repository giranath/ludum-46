using Godot;
using System;

public enum DoorState
{
	Opened,
	Closed
}

public class SOBaseDoor : SmartObject
{
	[Export]
	public DoorState currentState;

	[Export]
	public bool locked;

	[Export]
	public int requiredKey = -1;

	[Signal]
	delegate void OnStateChanged(DoorState newState);

	private void TryToggleDoor(Item item)
	{
		// The door is not locked, we can toggle it
		if(!locked)
		{
			Toggle();
		}
		// The door is locked
		else
		{

		}
	}

	private void TryToggleLock(Item item)
	{
		// If the door is lockable
		if(requiredKey != -1)
		{
			// TODO: Get key associated with item
		}
		// The door is not lockable
		else
		{
			// TODO: Notify UI that interaction failed
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        base._Ready();
        
		EmitSignal(nameof(OnStateChanged), currentState);

		itemActionMap.Add(itemType.None, TryToggleDoor);
		itemActionMap.Add(itemType.AccessCard, TryToggleLock);
	}

	public void Open()
	{
		if(currentState == DoorState.Closed)
		{
			currentState = DoorState.Opened;
			EmitSignal(nameof(OnStateChanged), currentState);
		}
	}

	public void Close()
	{
		if(currentState == DoorState.Opened)
		{
			currentState = DoorState.Closed;
			EmitSignal(nameof(OnStateChanged), currentState);
		}
	}

	public void Toggle()
	{
		if(currentState == DoorState.Closed)
		{
			Open();
		}
		else
		{
			Close();
		}
	}
}

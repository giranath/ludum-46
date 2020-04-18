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

    [Signal]
    delegate void LockedStateChanged(bool newLockedState);

    [Signal]
    delegate void InteractionFailed();

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
            EmitSignal(nameof(InteractionFailed));
		}
	}

	private void TryToggleLock(Item item)
	{
        AccessCard card = item as AccessCard;

		// If the door is lockable
		if(requiredKey != -1 && card.key == requiredKey)
		{
            locked = !locked;
            EmitSignal(nameof(LockedStateChanged), locked);
		}
		// The door is not lockable
		else
		{
            EmitSignal(nameof(InteractionFailed));
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

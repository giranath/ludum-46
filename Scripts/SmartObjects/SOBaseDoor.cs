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

    [Signal]
    delegate void OnStateChanged(DoorState newState);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        EmitSignal(nameof(OnStateChanged), currentState);
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

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

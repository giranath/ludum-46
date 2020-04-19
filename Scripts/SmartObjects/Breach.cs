using Godot;
using System;

public class Breach : SmartObject, Destroyable
{
    private Sprite breachSprite;
    private Sprite repairedSprite;

    [Export]
    NodePath roomPath;

    [Export]
    float OxygenWithdrawableValue;

    private Room room;

    private TimedRepeater oxygenWithdrawalRepeater;

    [Signal]
    public delegate void OnInteractionInvalid();

    enum State
    {
        Intact,
        Active,
        Repaired
    }

    State currentState = State.Intact;

    private void TryRepair(Item item)
    {
        if(currentState == State.Active)
        {
            ChangeState(State.Repaired);
        }
        else
        {
            EmitSignal(nameof(OnInteractionInvalid));
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        breachSprite = GetNode<Sprite>("./BreachSprite");
        repairedSprite = GetNode<Sprite>("./RepairSprite");

        itemActionMap.Add(itemType.ToolBox, TryRepair);

        room = GetNode<Room>(roomPath);

        oxygenWithdrawalRepeater = new TimedRepeater(1.0f, 0, WithdrawOxygenFromRoom);

        GameState gameState = GetNode<GameState>("/root/GameState");
        gameState.destroyables.Add(this);
    }

    private void WithdrawOxygenFromRoom(int count)
    {
        if(currentState == State.Active)
        {
            room.Graph.TryAddPropertyOfRoom(room, "oxygen", -OxygenWithdrawableValue);
        }
    }

    private void ChangeState(State newState)
    {
        currentState = State.Repaired;

        switch(currentState)
        {
            case State.Active:
                breachSprite.Visible = true;
                repairedSprite.Visible = false;
                break;
            case State.Intact:
                breachSprite.Visible = false;
                repairedSprite.Visible = false;
                break;
            case State.Repaired:
                breachSprite.Visible = true;
                repairedSprite.Visible = true;
                break;
        }
    }

     public void Destroy()
     {
         ChangeState(State.Active);
     }

    public void Repair(Item value)
    {
        // DO nothing
    }
}

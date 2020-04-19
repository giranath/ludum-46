using Godot;
using System;

public class Breach : SmartObject
{
    private Sprite breachSprite;
    private Sprite repairedSprite;

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

        itemActionMap.Add(itemType.DuckTape, TryRepair);
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
}

using Godot;
using System;

public class CharacterStateClimbingIdle : CharacterStates
{
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }

    public override CharacterStates HandleInput(CharacterMovement characterMovement, float delta)
    {
        if (Input.IsActionPressed(Ascend) || Input.IsActionPressed(Descend))
        {
            return new CharacterStateClimbing();
        }

        if (Input.IsActionPressed(MoveRight) || Input.IsActionPressed(MoveLeft))
        {
            if (characterMovement.CanClimb)
            {
                return new CharacterStateClimbing();
            }

            return new CharacterStateMoving();
        }

        return this;
    }
}

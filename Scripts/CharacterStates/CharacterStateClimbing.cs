using Godot;
using System;

public class CharacterStateClimbing : CharacterStates
{
    private float Speed = 60f;

    public override void _Process(float delta)
    {

    }

    public override CharacterStates HandleInput(CharacterMovement characterMovement, float delta)
    {
        var body = characterMovement.GetNode<KinematicBody2D>(characterMovement.Body);

        if (Input.IsActionPressed(Ascend) && characterMovement.CanClimb)
        {
            characterMovement.Velocity.y = -Speed;
            body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
            return this;
        }

        if (Input.IsActionPressed(Descend) && characterMovement.CanClimb)
        {
            characterMovement.Velocity.y = Speed;
            body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
            return this;
        }

        if (Input.IsActionPressed(MoveLeft) && characterMovement.CanClimb)
        {
            characterMovement.Velocity.y = 0;
            characterMovement.Velocity.x = -Speed;
            body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
            return this;
        }

        if (Input.IsActionPressed(MoveRight) && characterMovement.CanClimb)
        {
            characterMovement.Velocity.y = 0;
            characterMovement.Velocity.x = Speed;
            body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
            return this;
        }

        if(!characterMovement.CanClimb)
        {
            return new CharacterStateIdle();
        }

        return new CharacterStateClimbingIdle();
    }
}

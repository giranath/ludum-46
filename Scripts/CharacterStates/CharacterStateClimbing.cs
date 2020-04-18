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
            body.MoveAndCollide(Vector2.Up * Speed * delta);
            return this;
        }

        if (Input.IsActionPressed(Descend) && characterMovement.CanClimb)
        {
            body.MoveAndCollide(Vector2.Down * Speed * delta);
            return this;
        }

        if (Input.IsActionPressed(MoveLeft) && characterMovement.CanClimb)
        {
            body.MoveAndCollide(Vector2.Left * Speed * delta);
            return this;
        }

        if (Input.IsActionPressed(MoveRight) && characterMovement.CanClimb)
        {
            body.MoveAndCollide(Vector2.Right * Speed * delta);
            return this;
        }

        if(!characterMovement.CanClimb)
        {
            return new CharacterStateIdle();
        }

        return new CharacterStateClimbingIdle();
    }
}

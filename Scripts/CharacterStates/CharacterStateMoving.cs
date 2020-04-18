using Godot;
using System;

public class CharacterStateMoving : CharacterStates
{
	private readonly float Speed = 120f;
    private readonly float JumpForce = 250f;

	public override void _Process(float delta)
	{

	}

	public override CharacterStates HandleInput(CharacterMovement characterMovement, float delta)
	{
		var body = characterMovement.GetNode<KinematicBody2D>(characterMovement.Body);
        
        if (Input.IsActionPressed(Jump) && body.IsOnFloor())
        {
            characterMovement.Velocity.y = -JumpForce;
        }
        else if (!body.IsOnFloor())
        {
            characterMovement.Velocity += Gravity * delta;
        }
        else
        {
            characterMovement.Velocity.y = 0;
        }

        if ((Input.IsActionPressed(Ascend) || Input.IsActionPressed(Descend)) && characterMovement.CanClimb)
		{
			return new CharacterStateClimbing();
		}

		if (Input.IsActionPressed(MoveLeft))
		{         
            characterMovement.Velocity.x = -Speed;

			body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
			return this;
		}

		if (Input.IsActionPressed(MoveRight))
        {
            characterMovement.Velocity.x = Speed;

            body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
            return this;
		}

        return new CharacterStateIdle();
	}
}


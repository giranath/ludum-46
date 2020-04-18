using Godot;
using System;

public class CharacterStateMoving : CharacterStates
{
	private float Speed = 120f;

	public override void _Process(float delta)
	{

	}

	public override CharacterStates HandleInput(CharacterMovement characterMovement, float delta)
	{
		var body = characterMovement.GetNode<KinematicBody2D>(characterMovement.Body);

		if ((Input.IsActionPressed(Ascend) || Input.IsActionPressed(Descend)) && characterMovement.CanClimb)
		{
			return new CharacterStateClimbing();
		}

		if (Input.IsActionPressed(MoveLeft))
		{
            if(!body.IsOnFloor())
            {
                characterMovement.Velocity += Gravity * delta;
            }
            else
            {
                characterMovement.Velocity.y = 0;
            }

            characterMovement.Velocity.x = -Speed;

			body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
			return this;
		}

		if (Input.IsActionPressed(MoveRight))
        {
            if (!body.IsOnFloor())
            {
                characterMovement.Velocity += Gravity * delta;
            }
            else
            {
                characterMovement.Velocity.y = 0;
            }

            characterMovement.Velocity.x = Speed;

            body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
            return this;
		}        

		return new CharacterStateIdle();
	}
}


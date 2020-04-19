using Godot;
using System;

public class CharacterStateClimbing : CharacterStates
{
	public override void _Process(float delta)
	{

	}

	public override CharacterStates HandleInput(CharacterMovement characterMovement, float delta)
    {
        var body = characterMovement.GetBody();

        if (Input.IsActionPressed(Ascend) && characterMovement.CanClimb())
		{
			characterMovement.Velocity.y = -characterMovement.ClimbingSpeed;
			body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
			return this;
		}

		if (Input.IsActionPressed(Descend) && characterMovement.CanClimb())
		{
			characterMovement.Velocity.y = characterMovement.ClimbingSpeed;
			body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
			return this;
		}

		if (Input.IsActionPressed(MoveLeft) && characterMovement.CanClimb())
		{
			characterMovement.Velocity.y = 0;
			characterMovement.Velocity.x = -characterMovement.ClimbingSpeed;
			body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
			return this;
		}

		if (Input.IsActionPressed(MoveRight) && characterMovement.CanClimb())
		{
			characterMovement.Velocity.y = 0;
			characterMovement.Velocity.x = characterMovement.ClimbingSpeed;
			body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
			return this;
		}

		if(!characterMovement.CanClimb())
        {
            characterMovement.Velocity.y = 0;
            body.MoveAndSlide(characterMovement.Velocity, Vector2.Up);
            return new CharacterStateIdle();
		}

		return new CharacterStateClimbingIdle();
	}
}

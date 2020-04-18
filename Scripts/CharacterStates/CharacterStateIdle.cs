using Godot;

public class CharacterStateIdle : CharacterStates
{
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
    }

    public override CharacterStates HandleInput(CharacterMovement characterMovement, float delta)
    {
        var body = characterMovement.GetNode<KinematicBody2D>(characterMovement.Body);

        if (Input.IsActionPressed(MoveLeft) || Input.IsActionPressed(MoveRight))
        {
            return new CharacterStateMoving();
        }

        if ((Input.IsActionPressed(Ascend) || Input.IsActionPressed(Descend)) && characterMovement.CanClimb)
        {
            return new CharacterStateClimbing();
        }

        if (!body.IsOnFloor())
        {
            characterMovement.Velocity += Gravity * delta;
        }
        else
        {
            characterMovement.Velocity.y = 0;
        }


        body.MoveAndSlide(characterMovement.Velocity);
        return this;
    }
}

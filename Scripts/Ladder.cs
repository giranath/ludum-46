using Godot;
using System;

public class Ladder : Node
{
	public override void _Ready()
	{

	}

	public void OnBodyEntered(object body)
	{
		var characterMovement = ((Node)body) as CharacterMovement;

		if (characterMovement != null)
		{
			characterMovement.Climb += 1;
		}
	}
	public void OnBodyExited(object body)
    {
        var characterMovement = ((Node)body) as CharacterMovement;

        if (characterMovement != null)
		{
			characterMovement.Climb -= 1;
		}
	}
}

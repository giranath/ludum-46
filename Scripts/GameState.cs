using Godot;
using System;

public class GameState : Node
{
    public CharacterMovement player;

    public float fuel { get; set; } = 75;
}

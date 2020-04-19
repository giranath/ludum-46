using Godot;
using System;

public class FoetusRocking : Sprite
{
    [Export]
    float horizontalOffset;

    private float acc = 0.0f;

    public override void _Ready()
    {
        base._Ready();

        GameState gamestate = GetNode<GameState>("/root/GameState");

        acc = gamestate.RandomFloat();
    }

    public override void _Process(float delta)
    {
        acc += delta;
        float sin = Mathf.Sin(acc);

        Position = new Vector2(sin * horizontalOffset, 0.0f);
    }
}

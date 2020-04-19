using Godot;
using System;

public class BackgroundSlide : Node {

    [Export] public NodePath backgroundPart1;
    [Export] public NodePath backgroundPart2;
    [Export] public float slidingSpeed;
    [Export] public float momentum = 0.8f;

    private Sprite background1Sprite;
    private Sprite background2Sprite;

    private GameState gameState;

    private float previousCruiseSpeed = -1.0f;

    public override void _Ready()
    {
        base._Ready();

        background1Sprite = GetNode<Sprite>(backgroundPart1);
        background2Sprite = GetNode<Sprite>(backgroundPart2);
        gameState = GetNode<GameState>("/root/GameState");
    }

    public override void _Process(float delta) {

        float currentCruiseSpeed = gameState.currentShipSpeed;

        float lerpCruiseSpeed = currentCruiseSpeed;
        if (previousCruiseSpeed >= 0.0f)
        {
            lerpCruiseSpeed = Mathf.Lerp(previousCruiseSpeed, currentCruiseSpeed, momentum * delta);
        }

        previousCruiseSpeed = lerpCruiseSpeed;

        background1Sprite.MoveLocalX(delta * lerpCruiseSpeed * slidingSpeed);
        background2Sprite.MoveLocalX(delta * lerpCruiseSpeed * slidingSpeed);

        if (background1Sprite.Position.x < -1700) {
            background1Sprite.Position = new Vector2(9600, 0);
        }

        if (background2Sprite.Position.x < -1700) {
            background2Sprite.Position = new Vector2(9600, 0);
        }
    }
}

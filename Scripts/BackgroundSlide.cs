using Godot;
using System;

public class BackgroundSlide : Node {

    [Export] public NodePath backgroundPart1;
    [Export] public NodePath backgroundPart2;
    [Export] public float slidingSpeed;

    public override void _Ready()
    {
        
    }
    
    public override void _Process(float delta) {
        ((Sprite) GetNode(backgroundPart1)).MoveLocalX(delta * slidingSpeed);
        ((Sprite) GetNode(backgroundPart2)).MoveLocalX(delta * slidingSpeed);

        if (((Node2D) GetNode(backgroundPart1)).Position.x < -4000) {
            ((Node2D) GetNode(backgroundPart1)).Position = new Vector2(6500,0);
            GD.Print("move");
        }
        if (((Node2D) GetNode(backgroundPart2)).Position.x < -4000) {
            ((Node2D) GetNode(backgroundPart2)).Position = new Vector2(6500,0);
            GD.Print("move");
        }
    }
}

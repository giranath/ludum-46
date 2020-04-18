using Godot;

public abstract class CharacterStates
{
    protected string MoveLeft { get; set; } = "move_left";

    protected string MoveRight { get; set; } = "move_right";

    protected string Ascend { get; set; } = "ascend";

    protected string Descend { get; set; } = "descend";

    protected Vector2 Gravity { get; set; } = Vector2.Down * 400;

    public abstract void _Process(float delta);

    public abstract CharacterStates HandleInput(CharacterMovement characterMovement, float delta);
}

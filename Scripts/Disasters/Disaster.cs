using Godot;

public abstract class Disaster : Node
{
	public abstract void Process();

	public abstract bool IsAvailable();
}

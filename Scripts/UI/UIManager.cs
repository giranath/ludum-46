using Godot;
using System;

public class UIManager : Node
{
	[Export]
	public NodePath DialogUIPath;

	[Export]
	public NodePath ZonePromptUIPath;

	public DialogUI DialogUI;

	public DialogUI ZonePromptUI;

	public TimedRepeater bla;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DialogUI = GetNode<DialogUI>(DialogUIPath);
		ZonePromptUI = GetNode<DialogUI>(ZonePromptUIPath);
	}

	public override void _Process(float delta)
	{
	}
}

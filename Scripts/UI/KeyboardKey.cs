using Godot;
using System;

public class KeyboardKey : Node2D
{
	[Export]
	public string Key;

	[Export]
	public NodePath KeyPath;
	
	[Export]
	public string Description;

	[Export]
	public NodePath DescriptionPath;

	private Label KeyLabel;
	
	private Label DescriptionLabel;

	public override void _Ready()
	{
		KeyLabel = GetNode<Label>(KeyPath);
		DescriptionLabel = GetNode<Label>(DescriptionPath);
		
		KeyLabel.Text = Key;
		DescriptionLabel.Text = Description;
	}
}

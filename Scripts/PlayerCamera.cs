using Godot;
using System;

public class PlayerCamera : Node2D
{
	[Export]
	public NodePath playerPath;
	
	[Export]
	public NodePath viewportPath;

	[Export]
	public float DisasterCameraShakeDuration;

	[Export]
	public float shakeHRange;

	[Export]
	public float shakeVRange;

	[Export]
	public NodePath cameraPath;

	private Node2D playerNode;
	
	private float disasterShakeAcc = 0.0f;

	private bool isShakingForDisaster = false;

	private RandomNumberGenerator rand = new RandomNumberGenerator();

	private Camera2D camera;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        rand.Seed = Seed.Create();
        playerNode = GetNode<Node2D>(playerPath);
		camera = GetNode<Camera2D>(cameraPath);
	}

	public override void _PhysicsProcess(float delta)
	{
		GlobalPosition = playerNode.GlobalPosition;
	}

	public override void _Process(float delta)
	{
		if(isShakingForDisaster)
		{
			disasterShakeAcc += delta;

			camera.OffsetH = rand.RandfRange(-shakeHRange, shakeHRange);
			camera.OffsetV = rand.RandfRange(-shakeVRange, shakeVRange);

			if(disasterShakeAcc >= DisasterCameraShakeDuration)
			{
				disasterShakeAcc = 0.0f;
				isShakingForDisaster = false;
			}
		}
		else
		{
			camera.OffsetH = 0.0f;
			camera.OffsetV = 0.0f;
		}
	}

	public void OnDisaster()
	{
		isShakingForDisaster = true;
		disasterShakeAcc = 0.0f;
	}
}

using Godot;
using System;
using System.Collections.Generic;

public class GameState : Node
{
	[Export]
	public PackedScene victoryScene;

	[Export]
	public PackedScene defeatScene;

	public NodePath gameRootNode;

	public UIManager uiManager;

	public CharacterMovement player;

	public Station station;

    private float _fuel = 75;
    public float Fuel { 
        get { return _fuel; } 
        set { this._fuel = Mathf.Clamp(value, 0.0f, maxFuel); } 
    }

	public float maxFuel = 125;

	public float traveledDistance = 0.0f;

	public float requiredTravelDistance = 6000.0f;

	private bool destinationReached = false;

    public float currentShipSpeed = 0.0f;

    public List<Destroyable> destroyables = new List<Destroyable>();

	[Signal]
	public delegate void ReachedDestination();

    private RandomNumberGenerator rng = new RandomNumberGenerator();

    public override void _Ready()
    {
        rng.Seed = Seed.Create();
    }

	public float GetDistanceToTravel() {
		return Mathf.Clamp(requiredTravelDistance - traveledDistance, 0.0f, requiredTravelDistance);
	}

	public override void _Process(float delta) {
		if(traveledDistance >= requiredTravelDistance && !destinationReached) {
			EmitSignal(nameof(ReachedDestination));
			destinationReached = true;

			var treeRoot = GetTree().Root;
			treeRoot.RemoveChild(GetNode(gameRootNode));

			Node victorySceneNode = victoryScene.Instance();
			treeRoot.AddChild(victorySceneNode);
		}
	}

    public void OnMissionLost()
    {
		var treeRoot = GetTree().Root;
		treeRoot.RemoveChild(GetNode(gameRootNode));

		Node defeatSceneNode = defeatScene.Instance();
		treeRoot.AddChild(defeatSceneNode);
    }

    public float RandomFloat()
    {
        return rng.Randf();
    }
}

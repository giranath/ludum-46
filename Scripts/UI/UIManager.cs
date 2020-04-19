using Godot;
using System;

public class UIManager : CanvasLayer
{
	[Export]
	public NodePath DialogUIPath;

	[Export]
	public NodePath ZonePromptUIPath;

    [Export]
    public Color HighlightColor;

    [Export]
    public Color UnhilightColor;

	public DialogUI DialogUI;

	public DialogUI ZonePromptUI;

	public TimedRepeater bla;

    private TextureRect leftHandIcon;
    private TextureRect rightHandIcon;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DialogUI = GetNode<DialogUI>(DialogUIPath);
		ZonePromptUI = GetNode<DialogUI>(ZonePromptUIPath);
		
		var gameState = GetNode<GameState>("/root/GameState");
		gameState.uiManager = this;

        gameState.player.GetInventory().Connect("OnHandSelected", this, "OnHandSelected");

        leftHandIcon = GetNode<TextureRect>("InventoryHBox/LeftItemIcon");
        rightHandIcon = GetNode<TextureRect>("InventoryHBox/RightItemIcon");

        rightHandIcon.Modulate = HighlightColor;
        leftHandIcon.Modulate = UnhilightColor;
    }

	public override void _Process(float delta)
	{
	}

    public void OnRoomChanged(Room newRoom)
    {
        ZonePromptUI.SetText(5.0f, newRoom.roomName);
    }

    public void OnHandSelected(Inventory.Hand hand)
    {
        leftHandIcon.Modulate = UnhilightColor;
        rightHandIcon.Modulate = UnhilightColor;

        switch (hand)
        {
            case Inventory.Hand.Left:
                leftHandIcon.Modulate = HighlightColor;
                break;
            case Inventory.Hand.Right:
                rightHandIcon.Modulate = HighlightColor;
                break;
        }
    }
}

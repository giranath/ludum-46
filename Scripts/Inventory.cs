using Godot;
using System;

public class Inventory : Node {
    
	[Export] public NodePath right;
	[Export] public NodePath left;
    public NodePath groundItem;
    private Hand handUsed = Hand.Right;

    enum Hand {
        Right,
        Left
    }

    public override void _Process(float delta) {
        if (Input.IsActionJustPressed("useItemRight")) {
            handUsed = Hand.Right;
        }

        if (Input.IsActionJustPressed("useItemLeft")) {
            handUsed = Hand.Left;
        }

        if (Input.IsActionJustPressed("grabItem")) {
            if (handUsed == Hand.Right) {
                if (GetNode(right).GetChildren().Count == 0 && groundItem != null) {
                    Node2D groundObject = (Node2D) GetNode(groundItem);
                    groundObject.GetParent().RemoveChild(groundObject);
                    GetNode(right).AddChild(groundObject);
                    groundObject.Position = Vector2.Zero;
                    groundItem = null;
                } else if (GetNode(right).GetChildren().Count != 0) {
                    Item itemRight = (Item) GetNode(right).GetChild(0);
                    itemRight.throwItem((Node2D) GetParent());
                }
            } else if (handUsed == Hand.Left) {
                if (GetNode(left).GetChildren().Count == 0 && groundItem != null) {
                    Node2D groundObject = (Node2D) GetNode(groundItem);
                    groundObject.GetParent().RemoveChild(groundObject);
                    GetNode(left).AddChild(groundObject);
                    groundObject.Position = Vector2.Zero;
                    groundItem = null;
                } else if (GetNode(left).GetChildren().Count != 0) {
                    Item itemLeft = (Item) GetNode(left).GetChild(0);
                    itemLeft.throwItem((Node2D) GetParent());
                }
            }
        }
        
        if (Input.IsActionJustPressed("useItem")) {
            if (handUsed == Hand.Right && GetNode(right).GetChildren().Count > 0) {
                Item itemRight = (Item) GetNode(right).GetChild(0);
                itemRight.act();
            } else if (handUsed == Hand.Left && GetNode(left).GetChildren().Count > 0) {
                Item itemLeft = (Item) GetNode(left).GetChild(0);
                itemLeft.act();
            }
        }
    }
}

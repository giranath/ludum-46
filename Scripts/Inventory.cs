using Godot;
using System;

public class Inventory : Node {
	
	[Export] public NodePath right;
	[Export] public NodePath left;
	public NodePath groundItem;
	private Hand handUsed = Hand.Right;

    [Signal]
    public delegate void OnHandSelected(Hand newHand);

    [Signal]
    public delegate void ItemPicked();

    [Signal]
    public delegate void ItemDropped();

	public enum Hand {
		Right,
		Left
	}

	public Node2D GetHandNode(Hand hand) {
		if(hand == Hand.Left) {
			return GetNode<Node2D>(left);
		}
		else {
			return GetNode<Node2D>(right);
		}
	}

	public Item GetItemInHand(Hand hand) {
		Node2D handNode = GetHandNode(hand);

		if(handNode != null && handNode.GetChildCount() > 0) {
			return handNode.GetChild<Item>(0);
		}

		return null;
	}

	public Item GetCurrentItem() {
		return GetItemInHand(handUsed);
	}

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(float delta) {
		if (Input.IsActionJustPressed("useItemRight")) {
			handUsed = Hand.Right;
            EmitSignal(nameof(OnHandSelected), handUsed);
		}

		if (Input.IsActionJustPressed("useItemLeft")) {
			handUsed = Hand.Left;
            EmitSignal(nameof(OnHandSelected), handUsed);
		}

		if (Input.IsActionJustPressed("grabItem")) 
        {
			if (handUsed == Hand.Right) 
            {
				if (GetNode(right).GetChildren().Count == 0 && groundItem != null)
                {
                    Item groundObject = (Item)GetNode(groundItem);
                    groundObject.pickItem(GetNode(right));
                    groundItem = null;
                    EmitSignal(nameof(ItemPicked));
                } 
                else if (GetNode(right).GetChildren().Count != 0) 
                {
					Item itemRight = (Item) GetNode(right).GetChild(0);
					itemRight.throwItem((Node2D) GetParent());
                    EmitSignal(nameof(ItemDropped));
				}
			} 
            else if (handUsed == Hand.Left) 
            {
				if (GetNode(left).GetChildren().Count == 0 && groundItem != null) 
                {
                    Item groundObject = (Item) GetNode(groundItem);
                    groundObject.pickItem(GetNode(left));
					groundItem = null;
                    EmitSignal(nameof(ItemPicked));
                } 
                else if (GetNode(left).GetChildren().Count != 0) 
                {
					Item itemLeft = (Item) GetNode(left).GetChild(0);
					itemLeft.throwItem((Node2D) GetParent());
                    EmitSignal(nameof(ItemDropped));
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

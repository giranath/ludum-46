using Godot;
using System;
using System.Collections.Generic;

public enum RoomDirection
{
	Left,
	Right,
	Top,
	Bottom,
}

public class Room : Node2D
{
	[Export]
	public NodePath connectedLeftRoom;

	[Export]
	public NodePath connectedRightRoom;

	[Export]
	public NodePath connectedTopRoom;

	[Export]
	public NodePath connectedBottomRoom;

	[Export]
	public NodePath coveredAreaPath;

	[Export]
	public String roomName;

	[Signal]
	delegate void PlayerEntered(Room enteredRoom);

	[Signal]
	delegate void PlayerLeft(Room leftRoom);

	private RoomGraph owningGraph;

	// The RoomGraph is responsible to call this on ready
	public void SetOwningGraph(RoomGraph graph)
	{
		owningGraph = graph;
	}

	public RoomGraph Graph { get { return owningGraph; } }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (coveredAreaPath != null)
		{
			Area2D roomArea = GetNode<Area2D>(coveredAreaPath);

			if (roomArea != null)
			{
				GD.Print("Connecting Area 2D to signals");
				roomArea.Connect("body_entered", this, "OnBodyEnterRoom");
				roomArea.Connect("body_exited", this, "OnBodyExitRoom");
			}
			else
			{
				GD.PushWarning("Missing Room Area");
			}
		}
		else
		{
			GD.PushWarning("Missing Path to Room Area");
		}
	}
	private void OnBodyEnterRoom(Node body)
	{
		EmitSignal(nameof(PlayerEntered), this);
	}

	private void OnBodyExitRoom(Node body)
	{
		EmitSignal(nameof(PlayerLeft), this);
	}

	public void SetConnectionStateToRoom(RoomDirection direction, bool newConnectionState)
	{
		Room otherRoom = GetRoomInDirection(direction);
		if(owningGraph != null && otherRoom != null)
		{
			owningGraph.TrySetConnectionStatusBetweenRooms(this, otherRoom, newConnectionState);
		}
	}

	public Room GetRoomInDirection(RoomDirection direction)
	{
		switch (direction)
		{
			case RoomDirection.Left:
				return GetLeftRoom();
			case RoomDirection.Right:
				return GetRightRoom();
			case RoomDirection.Top:
				return GetTopRoom();
			case RoomDirection.Bottom:
				return GetBottomRoom();
			default:
				return null;
		}
	}

	public Room GetLeftRoom()
	{
		if (connectedLeftRoom != null)
		{
			return GetNode<Room>(connectedLeftRoom);
		}

		return null;
	}

	public Room GetRightRoom()
	{
		if (connectedRightRoom != null)
		{
			return GetNode<Room>(connectedRightRoom);
		}

		return null;
	}

	public Room GetTopRoom()
	{
		if (connectedTopRoom != null)
		{
			return GetNode<Room>(connectedTopRoom);
		}

		return null;
	}

	public Room GetBottomRoom()
	{
		if (connectedBottomRoom != null)
		{
			return GetNode<Room>(connectedBottomRoom);
		}

		return null;
	}

	public List<Room> GetConnectedRooms()
	{
		List<Room> connectedRooms = new List<Room>();

		Room leftRoom = GetLeftRoom();
		Room rightRoom = GetRightRoom();
		Room topRoom = GetTopRoom();
		Room bottomRoom = GetBottomRoom();

		if(leftRoom != null)
		{
			connectedRooms.Add(leftRoom);
		}

		if (rightRoom != null)
		{
			connectedRooms.Add(rightRoom);
		}

		if (topRoom != null)
		{
			connectedRooms.Add(topRoom);
		}

		if (bottomRoom != null)
		{
			connectedRooms.Add(bottomRoom);
		}

		return connectedRooms;
	}
}

using Godot;
using System;
using System.Collections.Generic;

public class RoomGraph : Node
{
	// The central room to create the graph from
	[Export]
	public NodePath centralRoom;

	[Signal]
	delegate void PlayerChangedRoom(Room enteringRoom);

	enum RoomConnectionState
	{
		Opened,
		Closed
	};

	class RoomConnection
	{
		public RoomNode otherNode;
		public RoomConnectionState state;
	};

	/**
	 * Provide rules for a property to propagate
	 */
	public interface IRoomPropertyProvider
	{
		float GetRoomPropertyInitialValue();

		float GetRoomPropertyMinValue();

		float GetRoomPropertyMaxValue();

		float ProcessRoomPropertyFromNeighbors(float delta, float current, float[] neighborsProperties, bool[] opened, Room[] rooms);
	}

	public class ExponentialRoomPropertyProvider : IRoomPropertyProvider
	{
		public float Momentum { get; set; }
		public float Decay { get; set; }

		public float GetRoomPropertyInitialValue()
		{
			return 0.0f;
		}

		public float GetRoomPropertyMaxValue()
		{
			return 100.0f;
		}

		public float GetRoomPropertyMinValue()
		{
			return 0.0f;
		}

		public float ProcessRoomPropertyFromNeighbors(float delta, float current, float[] neighborsProperties, bool[] opened, Room[] rooms)
		{
			float maxInfluence = float.MinValue;
			float minInfluence = float.MaxValue;

            bool atLeastOne = false;
            for(int i = 0; i < neighborsProperties.Length; ++i)
            {
                float neighbor = neighborsProperties[i];

                if(opened[i])
                {
                    // Decay neighbor influence based on distance
                    float influence = neighbor * Mathf.Exp(-Decay * delta);

                    maxInfluence = Mathf.Max(influence, maxInfluence);
                    minInfluence = Mathf.Min(influence, minInfluence);
                    atLeastOne = true;
                }
			}

            if (atLeastOne)
            {
                return Mathf.Lerp(current, maxInfluence, Momentum * delta);
            }
            else
            {
                return current;
            }
		}
	}

	/**
	 * Single node in the graph
	 */
	class RoomNode
	{
		public List<RoomConnection> connectedRooms = new List<RoomConnection>();
		public Room room;
		public List<float> propertyLayers = new List<float>();

		public RoomConnection FindConnectionToRoom(RoomNode otherNode)
		{
			foreach(RoomConnection connection in connectedRooms)
			{
				if(connection.otherNode == otherNode)
				{
					return connection;
				}
			}

			return null;
		}

		public bool IsConnectedTo(RoomNode otherNode)
		{
			RoomConnection connection = FindConnectionToRoom(otherNode);

			return connection != null;
		}
	};

	private List<RoomNode> nodes = new List<RoomNode>();
	private List<IRoomPropertyProvider> propertyProviders = new List<IRoomPropertyProvider>();
	private List<string> propertyNames = new List<string>();

	private RoomNode FindRoom(Room roomToSearch)
	{
		foreach (RoomNode node in nodes)
		{
			if (node.room == roomToSearch)
			{
				return node;
			}
		}

		return null;
	}

	private RoomNode FindOrAddRoom(Room roomToSearch)
	{
		foreach(RoomNode node in nodes)
		{
			if(node.room == roomToSearch)
			{
				return node;
			}
		}

		RoomNode newRoom = new RoomNode();
		newRoom.room = roomToSearch;

		foreach(IRoomPropertyProvider propertyProvider in propertyProviders)
		{
			newRoom.propertyLayers.Add(propertyProvider.GetRoomPropertyInitialValue());
		}

		nodes.Add(newRoom);

		return newRoom;
	}

	private bool ConnectRooms(Room a, Room b)
	{
		RoomNode nodeA = FindOrAddRoom(a);
		RoomNode nodeB = FindOrAddRoom(b);

		bool connected = false;
		if(!nodeA.IsConnectedTo(nodeB))
		{
			RoomConnection newConnection = new RoomConnection();
			newConnection.otherNode = nodeB;
			newConnection.state = RoomConnectionState.Opened;

			nodeA.connectedRooms.Add(newConnection);
			connected = true;
		}

		if (!nodeB.IsConnectedTo(nodeA))
		{
			RoomConnection newConnection = new RoomConnection();
			newConnection.otherNode = nodeA;
			newConnection.state = RoomConnectionState.Opened;

			nodeB.connectedRooms.Add(newConnection);
			connected = true;
		}

		return connected;
	}

	public bool TrySetConnectionStatusBetweenRooms(Room a, Room b, bool connectionStatus)
	{
		RoomNode nodeA = FindRoom(a);
		RoomNode nodeB = FindRoom(b);

		if(nodeA != null && nodeB != null)
		{
			RoomConnection connectionA = nodeA.FindConnectionToRoom(nodeB);
			RoomConnection connectionB = nodeB.FindConnectionToRoom(nodeA);

			if(connectionA != null)
			{
				connectionA.state = connectionStatus ? RoomConnectionState.Opened : RoomConnectionState.Closed;
				connectionB.state = connectionStatus ? RoomConnectionState.Opened : RoomConnectionState.Closed;

				return true;
			}
		}

		return false;
	}

	private void AddRoomToGraph(Room room)
	{
		// This graph takes ownership of the room
		room.SetOwningGraph(this);

		RoomNode node = FindOrAddRoom(room);

		List<Room> connectedRooms = room.GetConnectedRooms();

		foreach(Room connectedRoom in connectedRooms)
		{
			if(ConnectRooms(room, connectedRoom))
			{
				AddRoomToGraph(connectedRoom);
			}
		}
	}

	public void RegisterNewRoomProperty(string propertyName, IRoomPropertyProvider roomPropertyProvider)
	{
		propertyProviders.Add(roomPropertyProvider);
		propertyNames.Add(propertyName);

		// Update every nodes
		foreach(RoomNode node in nodes)
		{
			node.propertyLayers.Add(roomPropertyProvider.GetRoomPropertyInitialValue());
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Need to setup correctly
		ExponentialRoomPropertyProvider provider = new ExponentialRoomPropertyProvider();
		provider.Decay = 0.6f;
		provider.Momentum = 0.4f;

		RegisterNewRoomProperty("oxygen", provider);

		Room centralRoomNode = GetNode<Room>(centralRoom);


		if(centralRoomNode != null)
		{
			AddRoomToGraph(centralRoomNode);

			foreach(RoomNode node in nodes)
			{
				GD.Print("Node ", node.room.Name, " has ", node.connectedRooms.Count, " connections");
			}
		}
		else
		{
			GD.PushError("No central room defined");
		}

		foreach(RoomNode node in nodes)
		{
			node.room.Connect("PlayerEntered", this, "OnEnterRoom");
		}

		TrySetPropertyOfRoom(centralRoomNode, "oxygen", 100.0f);
	}

	private void OnEnterRoom(Room enteredRoom)
	{
		EmitSignal(nameof(PlayerChangedRoom), enteredRoom);
	}

	public bool TrySetPropertyOfRoom(Room room, int propertyIdx, float value)
	{
		RoomNode node = FindRoom(room);

		if(node != null)
		{
			IRoomPropertyProvider provider = propertyProviders[propertyIdx];
			node.propertyLayers[propertyIdx] = Mathf.Clamp(value, provider.GetRoomPropertyMinValue(), provider.GetRoomPropertyMaxValue());

			return true;
		}

		return false;
	}

	public bool TrySetPropertyOfRoom(Room room, string propertyName, float value)
	{
		int propertyIdx = propertyNames.IndexOf(propertyName);
		
		if(propertyIdx >= 0)
		{
			return TrySetPropertyOfRoom(room, propertyIdx, value);
		}

		return false;
	}

	public bool TryResetPropertyOfRoom(Room room, Int32 propertyIdx)
	{
		RoomNode node = FindRoom(room);

		if (node != null)
		{
			IRoomPropertyProvider provider = propertyProviders[propertyIdx];

			node.propertyLayers[propertyIdx] = provider.GetRoomPropertyInitialValue();

			return true;
		}

		return false;
	}

	public bool TryResetPropertyOfRoom(Room room, string propertyName)
	{
		int propertyIdx = propertyNames.IndexOf(propertyName);

		if (propertyIdx >= 0)
		{
			return TryResetPropertyOfRoom(room, propertyIdx);
		}

		return false;
	}

	public bool TryAddPropertyOfRoom(Room room, Int32 propertyIdx, float value)
	{
		RoomNode node = FindRoom(room);

		if (node != null)
		{
			IRoomPropertyProvider provider = propertyProviders[propertyIdx];

			node.propertyLayers[propertyIdx] = Mathf.Clamp(node.propertyLayers[propertyIdx] + value, provider.GetRoomPropertyMinValue(), provider.GetRoomPropertyMaxValue());

			return true;
		}

		return false;
	}

	public bool TryAddPropertyOfRoom(Room room, string propertyName, float value)
	{
		int propertyIdx = propertyNames.IndexOf(propertyName);

		if (propertyIdx >= 0)
		{
			return TryAddPropertyOfRoom(room, propertyIdx, value);
		}

		return false;
	}

	public bool TryGetRoomProperty(Room room, int propertyIdx, out float value)
	{
		value = 0.0f;

		RoomNode node = FindRoom(room);

		if(node != null)
		{
			value = node.propertyLayers[propertyIdx];

			return true;
		}

		return false;
	}

	public bool TryGetRoomProperty(Room room, string propertyName, out float value)
	{
		value = 0.0f;

		int propertyIdx = propertyNames.IndexOf(propertyName);

		if (propertyIdx >= 0)
		{
			return TryGetRoomProperty(room, propertyIdx, out value);
		}

		return false;
	}

	public void PropagateRoomProperties(float delta)
	{
		// Propagate every properties independently
		for (int propertyIdx = 0; propertyIdx < propertyProviders.Count; ++propertyIdx)
		{
			// Store current property layer
			float[] newProperties = new float[nodes.Count];

			// Propagate property
			for (int nodeIndex = 0; nodeIndex < nodes.Count; ++nodeIndex)
			{
				int neighborsCount = nodes[nodeIndex].connectedRooms.Count;
				float[] neighborsProperties = new float[neighborsCount];
				bool[] neighborsConnected = new bool[neighborsCount];
				Room[] neighborsRooms = new Room[neighborsCount];

				for(int neighborIndex = 0; neighborIndex < neighborsCount; ++neighborIndex)
				{
					neighborsRooms[neighborIndex] = nodes[nodeIndex].connectedRooms[neighborIndex].otherNode.room;
					neighborsProperties[neighborIndex] = nodes[nodeIndex].connectedRooms[neighborIndex].otherNode.propertyLayers[propertyIdx];
					neighborsConnected[neighborIndex] = nodes[nodeIndex].connectedRooms[neighborIndex].state == RoomConnectionState.Opened;
				}

				// It's up to Process Room
				IRoomPropertyProvider propertyProvider = propertyProviders[propertyIdx];
				float resultingProperty = propertyProvider.ProcessRoomPropertyFromNeighbors(delta, nodes[nodeIndex].propertyLayers[propertyIdx], neighborsProperties, neighborsConnected, neighborsRooms);

				newProperties[nodeIndex] = Mathf.Clamp(resultingProperty, propertyProvider.GetRoomPropertyMinValue(), propertyProvider.GetRoomPropertyMaxValue());
			}

			// Copy backbuffer into graph
			for (int nodeIndex = 0; nodeIndex < nodes.Count; ++nodeIndex)
			{
				nodes[nodeIndex].propertyLayers[propertyIdx] = newProperties[nodeIndex];
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		PropagateRoomProperties(delta);
	}
}

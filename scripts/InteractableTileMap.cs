using Godot;
using System;

public partial class InteractableTileMap : TileMapLayer
{
	[Export]
	public Node2D CameraTarget;

	private  ColorRect _highlight;

	private Vector2I _tileDiv;
	private Vector2 _lastMousePos;
	private Vector2I _currentlyHighlightedCell;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_highlight = GetNode<ColorRect>("ColorRect");

		// Why 8!?!
		_tileDiv = (GetTileSet().TileSize / 8);
	}

	private void PlaceDebugTile()
	{
		SetPattern(_currentlyHighlightedCell, GetTileSet().GetPattern(0));
	}

	
	public override void _Input(InputEvent @event)
    {
        base._Input(@event);

		if (@event is InputEventMouseMotion mouseMotionEvent )
		{
			_lastMousePos = mouseMotionEvent.Position;
		}

		if(@event is InputEventMouseButton mbe)
		{
			if(mbe.ButtonIndex == MouseButton.Left && mbe.Pressed)
			{
				PlaceDebugTile();
			}
		}

    }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2	viewport = GetWindow().Size;
		Vector2 centerMousePos = _lastMousePos - (viewport / 2.0f);

		if(centerMousePos.X % GetTileSet().TileSize.X != 0)
		{
			centerMousePos.X += GetTileSet().TileSize.X / 2;
		}

		if(centerMousePos.Y % GetTileSet().TileSize.Y != 0)
		{
			centerMousePos.Y += GetTileSet().TileSize.Y;
		}
					
		_currentlyHighlightedCell = LocalToMap(CameraTarget.Position + (centerMousePos / _tileDiv));
		
		Vector2 mapWorldPos = MapToLocal(_currentlyHighlightedCell);
		_highlight.Position = mapWorldPos - (GetTileSet().TileSize / 2);
		GD.Print($"OnMouseMove : Centered Pos : {centerMousePos}, Viewport Size : {viewport}, Map World Pos : {mapWorldPos}. Tile Index : {_currentlyHighlightedCell}");
	}
}

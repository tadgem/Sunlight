using Godot;
using System;

public partial class CameraController : Camera2D
{

	[Export]
	public float FollowSpeed = 2.0f;

	[Export]
	public Node2D Target;


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = Position.Lerp(Target.Position, (float) delta * FollowSpeed); 
	}
}

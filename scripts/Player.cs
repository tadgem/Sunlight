using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float MovementSpeed = 5.0f;
    
	[Export]
	public float Snappiness = 5.0f;

	[Export]
	public float Deadzone = 0.2f;

	private AnimatedSprite2D _sprite;

	public override void _Ready()
    {
        base._Ready();
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		Vector2 input = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");

		if(input.X > Deadzone)
		{
			_sprite.Play("walk_horizontal");
		
		}

		if(input.X < -Deadzone)
		{
			_sprite.Play("walk_horizontal");
			_sprite.FlipH = true;
		}
		else
		{
			_sprite.FlipH = false;
		}		

		if(input.Y > Deadzone * 2.0f)
		{
			_sprite.Play("walk_down");
		}

		if(input.Y < -(Deadzone * 2.0f))
		{
			_sprite.Play("walk_up");
		}

		if(input.Length() < Deadzone)
		{
			_sprite.Stop();
		}

		Velocity += input * MovementSpeed;
		MoveAndSlide();

		Velocity += -Velocity * (float) delta * Snappiness;

    }
}

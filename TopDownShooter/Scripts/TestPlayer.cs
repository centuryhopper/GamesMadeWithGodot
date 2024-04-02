using Godot;
using System;

public partial class TestPlayer : CharacterBody2D
{
	[Export] private float Acceleration = 500f;
	[Export] private float Maxspeed = 200f;
	[Export] private float Friction = 500f;

	public override void _PhysicsProcess(double delta)
	{
		Move((float) delta);
	}
	
	public void Move(float delta)
	{
		// make sure the player doesn't move when opposing buttons are pressed
		var inputVector = Vector2.Zero;
		inputVector.X = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		inputVector.Y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		inputVector = inputVector.Normalized();
		if (inputVector != Vector2.Zero)
		{
			Velocity = Velocity.MoveToward(inputVector * Maxspeed, Acceleration * delta);
		}
		else
		{
			Velocity = Velocity.MoveToward(Vector2.Zero, Friction * delta);
		}
		MoveAndSlide();
	}
}

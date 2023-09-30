using Godot;
using System;

public partial class Bullet : Node2D
{
	[Export] float speed = 500f;
	[Export] float range = 1500f;
	[Export] private string area2dName = "BulletArea2D";
	[Export] private float damageAmount = 20f;


	private float distanceTravelled = 0;
	private Area2D area2D;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		area2D = GetNode<Area2D>(area2dName);
		area2D.BodyEntered += OnBodyEnter2D;
		area2D.AreaEntered += OnAreaEntered2D;

		
	}

	public override void _ExitTree()
	{
		// GD.Print("freeing up resources");
		area2D.BodyEntered -= OnBodyEnter2D;
		area2D.AreaEntered -= OnAreaEntered2D;
	}

	public void OnBodyEnter2D(Node2D other)
	{
		GD.Print("bullet body collided with " + other.Name);
		QueueFree();
	}

	// Define the method that will be called when the signal is emitted.
	private void OnAreaEntered2D(Area2D area)
	{
		GD.Print("bullet area collided with " + area.Name);
		if (area.GetParent<Enemy>() is Enemy enemy)
		{
			enemy.Health -= damageAmount;
		}
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float moveAmount = speed * (float)delta;
		Position += Transform.X.Normalized() * moveAmount;
		distanceTravelled += moveAmount;

		if (distanceTravelled > range)
		{
			QueueFree();
		}
	}
}

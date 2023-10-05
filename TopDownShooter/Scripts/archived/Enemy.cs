using Godot;
using System;

public partial class Enemy : Node2D
{
	[Export] private float speed = 100f;
	[Export] private float damageAmount = 10f;
	[Export] private string area2dName = "EnemyArea2D";
	[Export] private float health = 100f;
	public float Health
	{
		get => health;
		set
		{
			health = value;
			GD.Print(this.Name + ": " + health);
			if (health <= 0f)
			{
				QueueFree();
			}
		}
	}
	private PlayerCharacter player;
	private Timer timer;
	private Area2D area2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<PlayerCharacter>("../PlayerCharacter");
		timer = GetNode<Timer>("Timer");

		timer.Timeout += OnTimerTimeOut;
		area2D = GetNode<Area2D>(area2dName);
		if (area2D is null)
		{
			GD.Print("enemy area2d is null");
		}

		area2D.BodyEntered += OnBodyEnter2D;
		area2D.AreaEntered += OnAreaEntered2D;
		area2D.AreaExited += OnAreaExited2D;
	}

	public override void _ExitTree()
	{
		area2D.BodyEntered -= OnBodyEnter2D;
		area2D.AreaEntered -= OnAreaEntered2D;
		area2D.AreaExited -= OnAreaExited2D;
	}

	private void OnAreaExited2D(Area2D area)
	{
		if (area.GetParent() is PlayerCharacter player)
		{
			timer.Stop();
		}
	}

	private void OnTimerTimeOut()
	{
		if (player is not null)
		{
			player.Health -= damageAmount;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// move towards player
		float moveAmount = speed * (float)delta;
		var moveDirection = (player.Position - Position).Normalized();
		Position += moveDirection * moveAmount;


		LookAt(player.Position);
	}


    public void OnBodyEnter2D(Node2D other)
	{
		GD.Print("enemy body collided with " + other.Name);
		// QueueFree();
	}

	// Define the method that will be called when the signal is emitted.
	private void OnAreaEntered2D(Area2D area)
	{
		GD.Print("enemy area collided with " + area.Name);

		if (area.GetParent() is PlayerCharacter player)
		{
			// GD.Print(player.Name);
			// (area.GetParent() as Player).Health -= damageAmount;
			timer.Start(1);
		}
		// QueueFree();
	}
}

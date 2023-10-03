using Godot;
using System;

public partial class EnemyCharacter : CharacterBody2D
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

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<PlayerCharacter>("../PlayerCharacter");
		// timer = GetNode<Timer>("Timer");

		// timer.Timeout += OnTimerTimeOut;
	}

	// private void OnAreaExited2D(Area2D area)
	// {
	// 	if (area.GetParent() is PlayerCharacter player)
	// 	{
	// 		timer.Stop();
	// 	}
	// }

	// private void OnTimerTimeOut()
	// {
	// 	if (player is not null)
	// 	{
	// 		player.Health -= damageAmount;
	// 	}
	// }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// move towards player
		// float moveAmount = speed * (float)delta;
		// var moveDirection = (player.Position - Position).Normalized();
		// Position += moveDirection * moveAmount;


		// LookAt(player.Position);
	}

    public override void _PhysicsProcess(double delta)
    {
		MoveAndSlide();
    }


    // // Define the method that will be called when the signal is emitted.
    // private void OnAreaEntered2D(Area2D area)
    // {
    // 	GD.Print("enemy area collided with " + area.Name);

    // 	if (area.GetParent() is PlayerCharacter player)
    // 	{
    // 		// GD.Print(player.Name);
    // 		// (area.GetParent() as Player).Health -= damageAmount;
    // 		timer.Start(1);
    // 	}
    // 	// QueueFree();
    // }
}

using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;

public partial class EnemyCharacter : CharacterBody2D
{
	[Export] private float speed = 100f;
	[Export] private float damageAmount = 10f;
	[Export] private float health = 100f;
	private RayCast2D leftRayCast2D;
	private RayCast2D rightRayCast2D;

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

	[Export] private int DETECT_RADIUS = 100;
	[Export] private float FOV = 45f;

	private float angle = 0;
	private Vector2 direction = new Vector2();
	private Node2D FovEyes;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetTree().GetFirstNodeInGroup("player") as PlayerCharacter;
		FovEyes = GetNode<Node2D>("FirePt");
		// timer = GetNode<Timer>("Timer");

		// timer.Timeout += OnTimerTimeOut;

		// TODO: move raycast logic to only come into effect when in the idle state
		leftRayCast2D = GetNode<RayCast2D>("LeftRayCast");
		rightRayCast2D = GetNode<RayCast2D>("RightRayCast");
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
		if (leftRayCast2D.IsColliding())
		{
			// GD.Print("left collision at " + leftRayCast2D.GetCollisionPoint());
			var collidedObj = leftRayCast2D.GetCollider();
			if (collidedObj is PlayerCharacter)
			{
				GD.Print("left ray hit player");
			}
		}
		if (rightRayCast2D.IsColliding())
		{
			// GD.Print("right collision at " + rightRayCast2D.GetCollisionPoint());

			var collidedObj = rightRayCast2D.GetCollider();
			if (collidedObj is PlayerCharacter)
			{
				GD.Print("right ray hit player");
			}
		}
		// move towards player
		// float moveAmount = speed * (float)delta;
		// var moveDirection = (player.Position - Position).Normalized();
		// Position += moveDirection * moveAmount;


		// LookAt(player.Position);
	}

    public override void _PhysicsProcess(double delta)
    {
		MoveAndSlide();

		// var direction = 


    }

    public override void _Draw()
    {
		/*
		Godot Engine:

		X-Axis: Right is the positive direction. When you move an object to the right, you increase its X-coordinate.

		Y-Axis: Down is the positive direction. When you move an object downwards, you increase its Y-coordinate.

		Z-Axis: Forward into the screen is the positive direction. When you move an object forward (toward the camera), you increase its Z-coordinate. In 2D mode, the Z-axis is not used.

		Source: ChatGPT
		*/
		var basePt = FovEyes.Position;
		var deltaX = DETECT_RADIUS * Mathf.Sin(Mathf.DegToRad(FOV / 2));
        var deltaY = DETECT_RADIUS * Mathf.Cos(Mathf.DegToRad(FOV / 2));
		var rightPt = new Vector2(basePt.X + deltaX, basePt.Y + deltaY);
		var leftPt = new Vector2(basePt.X + deltaX, basePt.Y - deltaY);

		var centerPt = new Vector2(rightPt.X, basePt.Y);

        DrawLine(basePt, rightPt, Color.FromHsv(1.0f / 12.0f, 1.0f, 1.0f));

        DrawLine(basePt, leftPt, Color.FromHsv(1.0f / 12.0f, 1.0f, 1.0f));

        DrawLine(rightPt, centerPt, Color.FromHsv(1.0f / 12.0f, 1.0f, 1.0f));

        DrawLine(centerPt, leftPt, Color.FromHsv(1.0f / 12.0f, 1.0f, 1.0f));
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



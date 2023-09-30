using System.Collections;
using System.Collections.Generic;
using Godot;
using System;

public partial class Player : Node2D
{
	[Export] float speed = 500f;
	[Export] private float health = 100f;
	[Export] private string audioStreamPlayerName = "AudioStreamPlayer";
	// [Export] private string characterBody2DName = "PlayerCharacterBody2D";
	[Export] private string area2DName = "Area2D";
	public float Health
	{
		get => health;
		set
		{
			health = value;
			GD.Print(health);
			if (health <= 0f)
			{
				QueueFree();
			}
		}
	}
	private PackedScene bulletScene;

	private Node2D firePt;
	private AudioStreamPlayer audioStreamPlayer;
	// private CharacterBody2D characterBody2D;
	private Area2D area2D;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// characterBody2D = GetNode<CharacterBody2D>(characterBody2DName);
		area2D = GetNode<Area2D>(area2DName);
		area2D.AreaEntered += OnAreaEntered2D;
		area2D.AreaExited += OnAreaExited2D;

		bulletScene = GD.Load<PackedScene>("res://Scenes/Bullet.tscn");

		firePt = GetNode<Node2D>("FirePt");

		audioStreamPlayer = GetNode<AudioStreamPlayer>(audioStreamPlayerName);
		audioStreamPlayer.Stream = GD.Load<AudioStream>("res://Audio/barreta_m9-Dion_Stapper.wav");
	}

    private void OnAreaExited2D(Area2D area)
    {
        GD.Print("player leaving from " + area.Name);
    }

    private void OnAreaEntered2D(Area2D area)
    {
    	GD.Print("player collided with " + area.Name);
    }

    public override void _Process(double delta)
	{
		var moveVector = Vector2.Zero;
		var moveAmount = speed * (float)delta;
		bool up = Input.IsKeyPressed(Key.Up) || Input.IsKeyPressed(Key.W);
		bool down = Input.IsKeyPressed(Key.Down) || Input.IsKeyPressed(Key.S);
		bool left = Input.IsKeyPressed(Key.Left) || Input.IsKeyPressed(Key.A);
		bool right = Input.IsKeyPressed(Key.Right) || Input.IsKeyPressed(Key.D);

		if (up)
		{
			moveVector.Y = -1;
		}
		if (down)
		{
			moveVector.Y = 1;
		}
		if (left)
		{
			moveVector.X = -1;
		}
		if (right)
		{
			moveVector.X = 1;
		}

		// move the player
		// normalize before multiplying to keep a uniform speed
		// before the diagonal movements were faster than just any horizontal or vertical directions
		this.Position += moveVector.Normalized() * moveAmount;

		// face the mouse
		Rotation = (GetGlobalMousePosition() - GlobalPosition).Angle();

	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				// GD.Print("left mouse button pressed!");

				var bullet = (Bullet)bulletScene.Instantiate();

				bullet.Position = firePt.GlobalPosition;
				bullet.Rotation = firePt.GlobalRotation;

				audioStreamPlayer.Play();

				GetTree().Root.AddChild(bullet);

				GetViewport().SetInputAsHandled();
			}
		}
	}
}

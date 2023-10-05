using Godot;
using System;

public partial class MouseCursor : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var crosshair = GD.Load("res://Images/crosshair.png");
		// GD.Print(crosshair.ResourcePath);
		// GD.Print("hello from mouse");
		Input.SetCustomMouseCursor(crosshair, Input.CursorShape.Arrow);
	}
}

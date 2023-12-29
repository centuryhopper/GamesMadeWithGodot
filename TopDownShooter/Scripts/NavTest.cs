using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class NavTest : Node2D
{
	private List<Vector2> path;
	private Line2D line2D;
	private Sprite2D sprite2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		line2D = GetNode<Line2D>("Line2D");
		sprite2D = GetNode<Sprite2D>("Sprite2D");
	}

	private void MoveAlongPath()
	{
		foreach(var pt in path)
		{
			line2D.AddPoint(pt);
			line2D.DefaultColor = Color.FromHsv(1.0f / 12.0f, 1.0f, 1.0f);
			line2D.Width = 5;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsMouseButtonPressed(MouseButton.Left))
		{
			var from = sprite2D.Position;
			var to = GetGlobalMousePosition();
			var navMap = GetWorld2D().NavigationMap;
			path = NavigationServer2D.MapGetPath(navMap, from, to, true).ToList();
			GD.Print(path.Count);
			line2D.ClearPoints();
			MoveAlongPath();
		}
	}
}

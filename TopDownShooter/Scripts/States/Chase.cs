using Godot;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class Chase : State
{
	[Export] private CharacterBody2D Enemy;
	[Export] private float ChaseSpeed = 200f;
    [Export] private Timer Timer;
	[Export] private Line2D line2D;
	private CharacterBody2D Player;

    public override void Enter()
    {
        Player = GetTree().GetFirstNodeInGroup("player") as CharacterBody2D;

		GD.Print("entering chase");
    }

	private List<Vector2> path;

	private void MoveAlongPath()
	{
		foreach(var pt in path)
		{
			line2D.AddPoint(pt);
			line2D.DefaultColor = Color.FromHsv(1.0f / 12.0f, 1.0f, 1.0f);
			line2D.Width = 5;
		}
	}

	public override void Update(double delta)
	{
		var from = Enemy.Position;
		var to = GetGlobalMousePosition();
		var navMap = GetWorld2D().NavigationMap;
		path = NavigationServer2D.MapGetPath(navMap, from, to, true).ToList();
		// GD.Print(path.Count);
		line2D.ClearPoints();
		MoveAlongPath();
	}

    public override void PhysicsUpdate(double delta)
    {
		// if (navigationAgent2D.IsTargetReachable())
		// {
		// 	var nextPoint = navigationAgent2D.GetNextPathPosition();
		// 	var dir = Player.GlobalPosition.DirectionTo(nextPoint);
		// 	Enemy.GlobalPosition += dir * ChaseSpeed * (float) delta;
		// }

		// if (navigationAgent2D.IsNavigationFinished())
		// {
			
		// }

		// Enemy.Velocity = Enemy.Velocity.Lerp(dir * ChaseSpeed, 70 * (float) delta);
		Enemy.MoveAndSlide();

        // var direction = Target.GlobalPosition - Enemy.GlobalPosition;

		// var dist = Enemy.GlobalPosition.DistanceSquaredTo(Player.GlobalPosition);

		// // GD.Print(dist);

		// if (dist < 100_000)
		// {
		// 	EmitSignal(SignalName.TransitionSignal, this, "idle");
		// }

		// // GD.Print(dist);

		// if (dist > 100_000)
		// {
		// 	Enemy.Position += Enemy.Position.DirectionTo(Target.Position) * ChaseSpeed * (float) delta;
		// }
		// else
		// {
		// 	// player has ran off
		// 	// GD.Print("stopping chase");
		// 	Enemy.Position += Vector2.Zero;
		// }

		// if (dist > 800_000)
		// {
		// 	// GD.Print("going to idle");
		// 	EmitSignal(SignalName.TransitionSignal, this, "idle");
		// }
    }

    public override void Exit()
    {
        // GD.Print("leaving chase state");
        // Timer.Timeout -= OnTimerTimeOut;
    }
}

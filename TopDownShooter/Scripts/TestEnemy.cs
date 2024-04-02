using Godot;
using System;

public partial class TestEnemy : CharacterBody2D
{
	[Export] private float Maxspeed = 150f;
	[Export] private Node2D Target = null;
	[Export] private NavigationAgent2D NavAgent = null;
	

	public override void _PhysicsProcess(double delta)
	{
		var direction = ToLocal(NavAgent.GetNextPathPosition()).Normalized();
		Velocity = direction * Maxspeed;
		MoveAndSlide();
	}
	
	public void CreatePath()
	{
		NavAgent.TargetPosition = Target.GlobalPosition;
	}
	
	private void _on_timer_timeout()
	{
		CreatePath();
	}
	
}






using Godot;
using System;

[GlobalClass]
public partial class Chase : State
{
	private CharacterBody2D Target;
	[Export] private CharacterBody2D Enemy;
	[Export] private float ChaseSpeed = 100f;

    public override void Enter()
    {
        Target = GetTree().GetFirstNodeInGroup("player") as CharacterBody2D;
		// GD.Print(Target.Name);
    }

    public override void PhysicsUpdate(double delta)
    {
        // var direction = Target.GlobalPosition - Enemy.GlobalPosition;

		var dist = Enemy.GlobalPosition.DistanceSquaredTo(Target.GlobalPosition);

		// GD.Print(dist);

		if (dist > 100_000)
		{
			Enemy.Position += Enemy.Position.DirectionTo(Target.Position) * ChaseSpeed * (float) delta;
		}
		else
		{
			// player has ran off
			// GD.Print("stopping chase");
			Enemy.Position += Vector2.Zero;
		}

		if (dist > 800_000)
		{
			// GD.Print("going to idle");
			EmitSignal(SignalName.TransitionSignal, this, "idle");
		}
    }

    public override void Exit()
    {
        // GD.Print("leaving chase state");
    }
}

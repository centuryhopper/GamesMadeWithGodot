using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Idle : State
{
    public Vector2 MoveDirection;
    public float WanderTime;
    [Export] public CharacterBody2D Enemy;
    [Export] public float MoveSpeed = 100f;

    private CharacterBody2D Target;

    private void RandomizeWander()
    {
        RandomNumberGenerator random = new();
        random.Randomize();
        MoveDirection = new Vector2(random.RandfRange(-10,10), random.RandfRange(-10,10)).Normalized();
        WanderTime = random.RandfRange(1,3);
    }

    public override void Enter()
    {
        Target = GetTree().GetFirstNodeInGroup("player") as CharacterBody2D;
        // GD.Print("entering idle state");
        RandomizeWander();
    }

    public override void Exit()
    {
        // GD.Print("leaving idle state");
    }

    public override void Update(double delta)
    {
        if (WanderTime > 0f)
        {
            WanderTime -= (float) delta;
        }
        else
        {
            RandomizeWander();
        }
    }

    public override void PhysicsUpdate(double delta)
    {
        if (Enemy is not null)
        {
            Enemy.Position += MoveDirection * MoveSpeed * (float)delta;
            // GD.Print(enemy.GlobalPosition);
        }

        var dist = Enemy.GlobalPosition.DistanceSquaredTo(Target.GlobalPosition);
        if (dist < 100_000f)
        {
            EmitSignal(SignalName.TransitionSignal, this, "chase");
        }
    }
}

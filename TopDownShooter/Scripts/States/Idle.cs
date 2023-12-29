using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Idle : State
{
    public Vector2 MoveDirection;
    public float WanderTime;
    [Export] private CharacterBody2D Enemy;
    [Export] private float MoveSpeed = 100f;
    private CharacterBody2D Player;
    private PathFollow2D PathFollow;
    private enum PatrolDirection
    {
        FORWARD,
        BACKWARD
    }
    private PatrolDirection patrolDirection;


    private void RandomizeWander()
    {
        RandomNumberGenerator random = new();
        random.Randomize();
        MoveDirection = new Vector2(random.RandfRange(-10, 10), random.RandfRange(-10, 10)).Normalized();
        WanderTime = random.RandfRange(1, 3);
    }

    public override void Enter()
    {
        Player = GetTree().GetFirstNodeInGroup("player") as CharacterBody2D;
        
        // PathFollow = Enemy.GetParent() as PathFollow2D;
    }

    public override void Exit()
    {
        GD.Print("leaving idle state");
    }

    public override void Update(double delta)
    {
    }

    public override async void PhysicsUpdate(double delta)
    {
        // if (patrolDirection == PatrolDirection.FORWARD)
        // {
        //     if (PathFollow.ProgressRatio == 1f)
        //     {
        //         await ToSignal(GetTree().CreateTimer(0.3f), "timeout");
        //         RotationDegrees = (float) Mathf.Lerp(RotationDegrees, 180, 0.1);
        //         await ToSignal(GetTree().CreateTimer(1f), "timeout");
        //         patrolDirection = PatrolDirection.BACKWARD;
        //     }
        //     else
        //     {
        //         PathFollow.Progress += MoveSpeed * (float) delta;
        //     }
        // }
        // else
        // {
        //     if (PathFollow.ProgressRatio == 0f)
        //     {
        //         await ToSignal(GetTree().CreateTimer(0.3f), "timeout");
        //         RotationDegrees = (float) Mathf.Lerp(RotationDegrees, 0, 0.1);
        //         await ToSignal(GetTree().CreateTimer(1f), "timeout");
        //         patrolDirection = PatrolDirection.FORWARD;
        //     }
        //     else
        //     {
        //         PathFollow.Progress -= MoveSpeed * (float) delta;
        //     }
        // }



        // Enemy.MoveAndSlide();

        // var dist = Enemy.GlobalPosition.DistanceSquaredTo(Player.GlobalPosition);
        // if (dist < 100_000f)
        // {
        //     EmitSignal(SignalName.TransitionSignal, this, "chase");
        // }
    }
}

using Godot;
using System;
using System.Collections.Generic;

public partial class Idle : State
{
    public Vector2 MoveDirection;
    public float WanderTime; 
    [Export] public CharacterBody2D enemy;
    [Export] public float MoveSpeed = 100f;


    private void RandomizeWander()
    {
        RandomNumberGenerator random = new();
        random.Randomize();
        MoveDirection = new Vector2(random.RandfRange(-10,10), random.RandfRange(-10,10)).Normalized();
        WanderTime = random.RandfRange(1,3);
    }

    public override void Enter()
    {
        // GD.Print("entering idle state");
        RandomizeWander();
    }

    public override void Exit()
    {
        throw new NotImplementedException();
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

        if (enemy is not null)
        {
            enemy.Position += MoveDirection * MoveSpeed * (float)delta;
            // GD.Print(enemy.GlobalPosition);
        }
    }
}

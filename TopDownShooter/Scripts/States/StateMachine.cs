using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node2D
{
	[Export] public State InitialState;
	public State CurrentState;
	public Dictionary<string, State> States;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		States = new();
		// initialize states
		foreach (var child in GetChildren())
		{
			if (child is State)
			{
				States[child.Name.ToString().ToLower()] = child as State;
				(child as State).OnTransition = OnChildTransition;
			}
		}

		if (InitialState is not null)
		{
			InitialState.Enter();
			CurrentState = InitialState;
		}
	}

    private void OnChildTransition(State state, string newStateName)
    {
        if (state != CurrentState)
		{
			return;
		}
		if (States.TryGetValue(newStateName.ToLower(), out State newState))
		{
			if (CurrentState is not null)
			{
				CurrentState.Exit();
			}

			newState.Enter();
		}
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		if (CurrentState is not null)
		{
			CurrentState.Update(delta);
		}
	}

    public override void _PhysicsProcess(double delta)
    {
        if (CurrentState is not null)
		{
			CurrentState.PhysicsUpdate(delta);
		}
    }
}

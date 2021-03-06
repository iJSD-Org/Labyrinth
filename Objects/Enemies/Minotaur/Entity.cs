using System.Collections.Generic;
using Labyrinth.Objects.Enemies.Minotaur.States;
using Godot;

namespace Labyrinth.Objects.Enemies.Minotaur
{
	public class Entity : KinematicBody2D
	{
		[Signal]
		public delegate void StateChanged();

		public State CurrentState;
		public Stack<State> StateStack = new Stack<State>();
		public readonly Dictionary<string, Node> StatesMap = new Dictionary<string, Node>();
		private KinematicBody2D _player;
		private int _charges;

		public override void _Ready()
		{
			GetNode<AudioStreamPlayer2D>("Roar").Play();
			StatesMap.Add("Chase", GetNode("States/Chase"));
			StatesMap.Add("Exhaust", GetNode("States/Exhaust"));
			StatesMap.Add("Charge", GetNode("States/Charge"));

			CurrentState = (Chase)GetNode("States/Chase");

			foreach (Node state in StatesMap.Values)
			{
				state.Connect(nameof(State.Finished), this, nameof(ChangeState));
			}

			_player = GetParent().GetNode<KinematicBody2D>("Player");
			StateStack.Push((State)StatesMap["Chase"]);
			ChangeState("Chase");
		}

		public override void _PhysicsProcess(float delta)
		{
			CurrentState.Update(this, delta);
			if (_charges == 4 && !GetNode<VisibilityNotifier2D>("VisibilityNotifier2D").IsOnScreen()) ChangeState("Dead");
		}

		private void ChangeState(string stateName)
		{
			GD.Print(stateName);
			CurrentState.Exit(this);
			if (stateName == "Dead")
			{
				QueueFree();
				return;
			}

			else if (stateName == "Exhaust")
			{
				StateStack.Push((State)StatesMap[stateName]);
			}

			else
			{
				StateStack.Pop();
				StateStack.Push((State)StatesMap[stateName]);
			}

			CurrentState = StateStack.Peek();

			// Pass target to Chase State
			if (stateName == "Chase")
			{
				((Chase)CurrentState).Init((Player.Entity)_player);
			}
			else if (stateName == "Charge")
			{
				_charges++;
				if (_charges > 4) _charges = 4;
				((Charge)CurrentState).Init((Player.Entity)_player);
			}

			// We don"t want to reinitialize the state if we"re going back to the previous state
			if (stateName != "Previous")
				CurrentState.Enter(this);

			EmitSignal(nameof(StateChanged), CurrentState.Name);
		}

		private void _on_ScreenTimer_timeout()
		{
			ChangeState("Dead");
		}
	}
}

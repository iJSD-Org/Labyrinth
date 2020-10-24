using Godot;
using Labyrinth.Objects.Player.States;
using System.Collections.Generic;

namespace Labyrinth.Objects.Player
{
	public class Entity : KinematicBody2D
	{
		[Signal]
		public delegate void StateChanged();

		public PackedScene ScentScene = ResourceLoader.Load<PackedScene>("res://Objects/Player/Scent.tscn");
		public List<Scent> ScentTrail = new List<Scent>();
		public State CurrentState;
		public Stack<State> StateStack = new Stack<State>();
		public readonly Dictionary<string, Node> StatesMap = new Dictionary<string, Node>();

		private Sprite _torch;
		public override void _Ready()
		{
			_torch = GetNode<Sprite>("Torch");
			StatesMap.Add("Move", GetNode("States/Move"));
			StatesMap.Add("Idle", GetNode("States/Idle"));

			CurrentState = (Idle)GetNode("States/Idle");

			foreach (Node state in StatesMap.Values)
			{
				state.Connect(nameof(State.Finished), this, nameof(ChangeState));
			}

			StateStack.Push((State)StatesMap["Idle"]);
			ChangeState("Idle");
		}     

		public override void _PhysicsProcess(float delta)
		{
			CurrentState.Update(this, delta);                    
		}

		private void ChangeState(string stateName)
		{
			CurrentState.Exit(this);
			if (stateName == "Dead")
			{
				Engine.TimeScale = .3f;
				GetNode<AnimationPlayer>("FadePlayer").Play("FadeOut");
				return;
			}
			else
			{
				StateStack.Pop();
				StateStack.Push((State)StatesMap[stateName]);
			}

			CurrentState = StateStack.Peek();

			// We don"t want to reinitialize the state if we"re going back to the previous state
			if (stateName != "Previous")
				CurrentState.Enter(this);

			EmitSignal(nameof(StateChanged), CurrentState.Name);
		}
		
		public void AddScent()
		{
			if(CurrentState != (Idle)GetNode("States/Idle"))
			{
				Scent scent = (Scent)ScentScene.Instance();
				scent.Position = Position;
				GetTree().Root.AddChild(scent);

				scent.Init(this);
				ScentTrail.Add(scent);
			}
		}

		public void _on_Area2D_body_entered(KinematicBody2D area)
		{
			if(area.IsInGroup("enemy"))
				ChangeState("Dead");
		}

		private void _on_FadePlayer_finished(string animation)
		{
			Engine.TimeScale = 1f;
			GetTree().ChangeScene("res://Levels/DeathScreen.tscn");
		}
	}
}

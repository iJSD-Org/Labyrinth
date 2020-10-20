using Godot;

namespace Labyrinth.Objects
{
	public abstract class State : Node
	{
		[Signal]
		public delegate void Finished(string nextStateName);
		public virtual void Enter(KinematicBody2D host) { }
		public virtual void Exit(KinematicBody2D host) { }
		public virtual void HandleInput(KinematicBody2D host, InputEvent @event) { }
		public virtual void Update(KinematicBody2D host, float delta) { }
	}
}

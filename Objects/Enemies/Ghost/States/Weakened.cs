using Godot;
using System;
using Labyrinth.Objects;

namespace Labyrinth.Objects.Enemies.Ghost.States
{
    public class Weakened : State
    {
		private readonly Random _random = new Random();

		public override void _Ready()
		{

		}

		public override void Enter(KinematicBody2D host)
		{
 
		}
		public override void Exit(KinematicBody2D host)
		{
			// Nothing to do here
		}

		public override void HandleInput(KinematicBody2D host, InputEvent @event)
		{
			// Nothing to do here
		}

		public override void Update(KinematicBody2D host, float delta)
		{

		}

		private void _on_WanderTimer_timeout()
		{
		
		}
    }
}

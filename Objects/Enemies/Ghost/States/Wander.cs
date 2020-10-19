using Godot;
using System;
using Labyrinth.Objects;

namespace Labyrinth.Objects.Enemies.Ghost.States
{
    public class Wander : State
    {
       [Export] public int Speed = 20;
		private Vector2 _dir;
		private Timer _wanderTimer;
        private Sprite _sprite;

		private readonly Random _random = new Random();

		public override void _Ready()
		{
            _wanderTimer = GetNode<Timer>("WanderTimer");
		}

		public override void Enter(KinematicBody2D host)
		{
            _sprite = GetParent().GetParent().GetNode<Sprite>("Sprite");
			host.GetNode<AnimationPlayer>("AnimationPlayer").Play("float");
			_wanderTimer.Start();
			_dir.x = _random.Next(-50, 50);
			_dir.y = _random.Next(-50, 50);
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
            _sprite.FlipH = _dir.x >= 0;
			host.MoveAndSlide(_dir.Normalized() * Speed);
		}

		private void _on_WanderTimer_timeout()
		{
			_wanderTimer.Stop();
			EmitSignal(nameof(Finished), "Wander");
		}
    }
}

using Godot;
using System;
using Labyrinth.Objects;

namespace Labyrinth.Objects.Enemies.Ghost.States
{
    public class Chase : State
    {
		private Vector2 _dir = Vector2.Zero;
		private KinematicBody2D _player;
		[Export] public float Speed = 0;
		[Export] public int MaxSpeed = 200;
		[Export] public int Acceleration = 700;
        public override void _Ready()
		{
			
		}

        public void Init(Player.Entity target)
		{
			Speed = 0;
			_player = target;
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
			Speed += Acceleration * delta;
			if (Speed > MaxSpeed) Speed = MaxSpeed;
			_dir = _player.GlobalPosition - host.GlobalPosition;
			host.MoveAndSlide(_dir.Normalized() * Speed);
		}
    }
}

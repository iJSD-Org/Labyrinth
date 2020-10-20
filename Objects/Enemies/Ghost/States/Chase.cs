using Godot;
using System;
using Labyrinth.Objects;

namespace Labyrinth.Objects.Enemies.Ghost.States
{
    public class Chase : State
    {
		private Vector2 _dir = Vector2.Zero;
		private KinematicBody2D _player;
		private float SPEED = 0;
		private int MAX_SPEED = 100;
		private int ACCELERATION = 40;
        public override void _Ready()
		{
			
		}

        public void Init(Player.Entity target)
		{
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
			SPEED += ACCELERATION * delta;
			if (SPEED > MAX_SPEED) SPEED = MAX_SPEED;
			_dir = _player.GlobalPosition - host.GlobalPosition;
			host.MoveAndSlide(_dir.Normalized() * SPEED);
		}
    }
}

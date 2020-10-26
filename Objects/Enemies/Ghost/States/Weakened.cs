using Godot;
using System;

namespace Labyrinth.Objects.Enemies.Ghost.States
{
	public class Weakened : State
	{
		[Export] private readonly float Speed = 20;
		private Vector2 _dir = Vector2.Zero;
		private KinematicBody2D _player;

		public void Init(Player.Entity target)
		{
			_player = target;
        }
		
		public override void Enter(KinematicBody2D host)
		{
			host.GetNode<AnimationPlayer>("AnimationPlayer").Play("float");
			_dir = _player.GlobalPosition - host.GlobalPosition;
		}

		public override void Update(KinematicBody2D host, float delta)
		{
			host.MoveAndSlide(_dir.Normalized() * Speed);
		}
	}
}

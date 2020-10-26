using Godot;
using System;
using Labyrinth.Objects.Player;

namespace Labyrinth.Objects.Enemies.Minotaur.States
{
	public class Chase : State
	{
		[Export] public int Speed = 50;
		private Player.Entity _target;
		private readonly Random _random = new Random();
		private Vector2 _direction = Vector2.Zero;
		private RayCast2D _look;

		public void Init(Player.Entity target)
		{
			_target = target;
			GetNode<Timer>("ChaseTimer").Start();
			GetNode<Timer>("ChaseTimer").WaitTime = (float)(_random.NextDouble() * (4.5 - 2.5) + 2.5);
		}

		public override void Enter(KinematicBody2D host)
		{
			_look = host.GetNode<RayCast2D>("RayCast2D");
			host.GetNode<AnimationPlayer>("AnimationPlayer").Play("chase");
			host.GetNode<AudioStreamPlayer2D>("Chase").Play();
		}

		public override void Update(KinematicBody2D host, float delta)
		{
			ChaseTarget(host);
			host.MoveAndSlide(_direction * Speed);
		}

		private void ChaseTarget(KinematicBody2D host)
		{		
			if (_target != null) _look.CastTo = _target.Position - host.Position;
			_look.ForceRaycastUpdate();

			// if we can see the target, chase it
			if (!_look.IsColliding() || ((Node)_look.GetCollider()).IsInGroup("player"))
			{
				_direction = _look.CastTo.Normalized();
			}
			// or chase the first scent we see
			else
			{
				foreach (Scent scent in _target.ScentTrail)
				{
					_look.CastTo = scent.Position - host.Position;
					_look.ForceRaycastUpdate();

					if (!_look.IsColliding() || ((Node)_look.GetCollider()).IsInGroup("player"))
					{
						_direction = _look.CastTo.Normalized();
						break;
					}
				}
			}
		}

		private void _on_ChaseTimer_timeout()
		{
			GetNode<Timer>("ChaseTimer").Stop();

			if (!_look.IsColliding() || ((Node)_look.GetCollider()).IsInGroup("player"))
			{
				EmitSignal(nameof(Finished), "Charge");
			}
			else
			{
				EmitSignal(nameof(Finished), "Chase");
			}
		}
	}
}

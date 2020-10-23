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

		public void Init(Player.Entity target)
		{
			_target = target;
			GetNode<Timer>("ChaseTimer").Start();
			GetNode<Timer>("ChaseTimer").WaitTime = (float)(_random.NextDouble() * (2.5 - 1.5) + 1.5);
		}

		public override void Enter(KinematicBody2D host)
		{
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
			RayCast2D look = host.GetNode<RayCast2D>("RayCast2D");

			if (_target != null) look.CastTo = _target.Position - host.Position;
			look.ForceRaycastUpdate();

			// if we can see the target, chase it
			if (!look.IsColliding() || ((Node)look.GetCollider()).IsInGroup("player"))
			{
				_direction = look.CastTo.Normalized();
			}
			// or chase the first scent we see
			else
			{
				foreach (Scent scent in _target.ScentTrail)
				{
					look.CastTo = scent.Position - host.Position;
					look.ForceRaycastUpdate();

					if (!look.IsColliding() || ((Node)look.GetCollider()).IsInGroup("player"))
					{
						_direction = look.CastTo.Normalized();
						break;
					}
				}
			}
		}

		private void _on_ChaseTimer_timeout()
		{
			RayCast2D look = GetOwner<KinematicBody2D>().GetNode<RayCast2D>("RayCast2D");

			GetNode<Timer>("ChaseTimer").Stop();

			if (!look.IsColliding() || ((Node)look.GetCollider()).IsInGroup("player"))
			{
				EmitSignal(nameof(Finished), "Charge");
			}
			else
			{
				GetNode<Timer>("ChaseTimer").Start();
			}
		}
	}
}

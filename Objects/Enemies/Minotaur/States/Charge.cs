using Godot;
using Labyrinth.Objects.Player;

namespace Labyrinth.Objects.Enemies.Minotaur.States
{
	public class Charge : State
	{
		[Export] public float Speed;
		[Export] public int Acceleration = 850;
		[Export] public int MaxSpeed = 240;

		private Player.Entity _target;
		private Vector2 _direction = Vector2.Zero;

		public void Init(Player.Entity target)
		{
			_target = target;
		}

		public override void Enter(KinematicBody2D host)
		{
			GetNode<Timer>("ChargeTimer").Start();
			_direction = _target.GlobalPosition - host.GlobalPosition;
			host.GetNode<AnimationPlayer>("AnimationPlayer").Play("charge");
			host.GetNode<AudioStreamPlayer2D>("Charge").Play();
		}

		public override void Update(KinematicBody2D host, float delta)
		{
			Speed += Acceleration * delta;
			if (Speed > MaxSpeed) Speed = MaxSpeed;

			ChaseTarget(host);
			host.MoveAndSlide(_direction.Normalized() * Speed);
		}

		public override void Exit(KinematicBody2D host)
		{
			host.GetNode<AudioStreamPlayer2D>("Charge").Stop();
		}

		private void _on_ChargeTimer_timeout()
		{
			GetNode<Timer>("ChargeTimer").Stop();
			EmitSignal(nameof(Finished), "Exhaust");
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
	}
}

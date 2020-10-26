using Godot;

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
			Speed = 0;
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
	}
}

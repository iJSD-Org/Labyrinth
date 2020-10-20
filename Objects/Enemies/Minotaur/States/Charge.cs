using Godot;

namespace Labyrinth.Objects.Enemies.Minotaur.States
{
    public class Charge : State
    {
        [Export] public float Speed;
        [Export] public int Acceleration = 850;
        [Export] public int MaxSpeed = 240;
        private KinematicBody2D _player;
        private Vector2 _dir = Vector2.Zero;

        public void Init(KinematicBody2D target)
        {
            Speed = 0;
            _player = target;
        }

        public override void Enter(KinematicBody2D host)
        {
            GetNode<Timer>("ChargeTimer").Start();
            _dir = _player.GlobalPosition - host.GlobalPosition;
            host.GetNode<AnimationPlayer>("AnimationPlayer").Play("charge");
        }

        public override void Update(KinematicBody2D host, float delta)
        {
            Speed += Acceleration * delta;
			if (Speed > MaxSpeed) Speed = MaxSpeed;
            host.MoveAndSlide(_dir.Normalized() * Speed);
        }

        private void _on_ChargeTimer_timeout()
        {
             GetNode<Timer>("ChargeTimer").Stop();
            EmitSignal(nameof(Finished), "Exhaust"); 
        }
    }
}

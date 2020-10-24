using Godot;

namespace Labyrinth.Objects.Enemies.Ghost.States
{
    public class Chase : State
    {
	    [Export] public float Speed;
	    [Export] public int MaxSpeed = 150;
	    [Export] public int Acceleration = 100;

		private KinematicBody2D _player;

		public void Init(Player.Entity target)
		{
			Speed = 0;
			_player = target;
        }

        public override void Update(KinematicBody2D host, float delta)
		{
			Speed += Acceleration * delta;
			if (Speed > MaxSpeed) Speed = MaxSpeed;
			host.MoveAndSlide((_player.GlobalPosition - host.GlobalPosition).Normalized() * Speed);
		}
    }
}

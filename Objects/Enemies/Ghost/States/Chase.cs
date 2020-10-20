using Godot;

namespace Labyrinth.Objects.Enemies.Ghost.States
{
    public class Chase : State
    {
	    [Export] public float Speed;
	    [Export] public int MaxSpeed = 200;
	    [Export] public int Acceleration = 700;

		private Vector2 _dir = Vector2.Zero;
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
			_dir = _player.GlobalPosition - host.GlobalPosition;
			host.MoveAndSlide(_dir.Normalized() * Speed);
		}
    }
}

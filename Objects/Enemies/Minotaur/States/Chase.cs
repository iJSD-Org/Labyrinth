using Godot;
using System;
using Labyrinth.Objects.Player;

namespace Labyrinth.Objects.Enemies.Minotaur.States
{
    public class Chase : State
    {
        [Export] public int Speed = 50;
        private Player.Entity _player;
        private readonly Random _random = new Random();
        private Vector2 _direction = Vector2.Zero;
        private RayCast2D _ray;

        public void Init(Player.Entity target)
		{
			_player = target;
            GetNode<Timer>("ChaseTimer").Start();
            GetNode<Timer>("ChaseTimer").WaitTime = (float)(_random.NextDouble() * (2.5 - 1.5) + 1.5);
		}

        public override void Enter(KinematicBody2D host)
        { 
           _ray = host.GetNode<RayCast2D>("RayCast2D");
           host.GetNode<AnimationPlayer>("AnimationPlayer").Play("chase");
        }

        public override void Update(KinematicBody2D host, float delta)
        {
            ChaseTarget(host);
            host.MoveAndSlide(_direction * Speed);
        }

        private void ChaseTarget(KinematicBody2D host)
		{
			if (_player != null) _ray.CastTo = _player.Position - host.Position; 
			_ray.ForceRaycastUpdate();

			// if we can see the target, chase it
			if (!_ray.IsColliding() || ((Node)_ray.GetCollider()).IsInGroup("player"))
			{
				_direction = _ray.CastTo.Normalized();
			}
			// or chase the first scent we see
			else
			{
				foreach (Scent scent in _player.ScentTrail)
				{
					_ray.CastTo = scent.Position - host.Position;
					_ray.ForceRaycastUpdate();

					if (!_ray.IsColliding() || ((Node)_ray.GetCollider()).IsInGroup("player"))
					{
						_direction = _ray.CastTo.Normalized();
						break;
					}
				}
			}
		}

        private void _on_ChaseTimer_timeout()
        {
            GetNode<Timer>("ChaseTimer").Stop();

            if (!_ray.IsColliding() || ((Node)_ray.GetCollider()).IsInGroup("player"))
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

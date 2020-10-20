using Godot;
using System;
using Labyrinth.Objects;
using System.Collections.Generic;

namespace Labyrinth.Objects.Enemies.Minotaur.States
{
    public class Chase : State
    {
        [Export] public int Speed = 50;
        private Navigation2D _nav2d;
        private KinematicBody2D _player;
        private Vector2[] _path = new Vector2[100];
        private Random _random = new Random();
        private RayCast2D _ray;
        private Sprite _sprite;
        public override void _Ready()
        {
          
        }
        public void Init(Player.Entity target, Navigation2D node)
		{
            _player = target;
            _nav2d = node;
            GetNode<Timer>("ChaseTimer").Start();
            GetNode<Timer>("ChaseTimer").WaitTime = (float)(_random.NextDouble() * (2.5 - 1.5) + 1.5);
		}
        public override void Enter(KinematicBody2D host)
        { 
           _ray = host.GetNode<RayCast2D>("RayCast2D");
           _sprite = host.GetNode<Sprite>("Sprite"); 
           host.GetNode<AnimationPlayer>("AnimationPlayer").Play("chase");
        }
        public override void Exit(KinematicBody2D host)
        {

        }
        public override void HandleInput(KinematicBody2D host, InputEvent @event)
        {

        }
        public override void Update(KinematicBody2D host, float delta)
        {
            _ray.CastTo = _player.Position - host.Position;
            _path = _nav2d.GetSimplePath(host.GlobalPosition, _player.GlobalPosition, true);
            List<Vector2> path = new List<Vector2>(_path);
            _sprite.FlipH = (_player.GlobalPosition - host.GlobalPosition).x <= 0;
            path.RemoveAt(0);
            host.MoveAndSlide((path[0] - host.GlobalPosition).Normalized() * Speed);
           // host.GlobalPosition = host.GlobalPosition.LinearInterpolate(path[0], (Speed * delta) / host.GlobalPosition.DistanceTo(path[0]));       
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

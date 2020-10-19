using Godot;
using System;
using Labyrinth.Objects;

namespace Labyrinth.Objects.Enemies.Minotaur.States
{
    public class Charge : State
    {
        private int SPEED = 125;
        private KinematicBody2D _player;
        private Vector2 _dir = Vector2.Zero;
        public override void _Ready()
        {
        
        }
        public void Init(KinematicBody2D target)
        {
            _player = target;
        }
        public override void Enter(KinematicBody2D host)
        {
            _dir = _player.GlobalPosition - host.GlobalPosition;
            GetParent().GetParent().GetNode<AnimationPlayer>("AnimationPlayer").Play("charge");
        }
        public override void Exit(KinematicBody2D host)
        {

        }
        public override void HandleInput(KinematicBody2D host, InputEvent @event)
        {

        }
        public override void Update(KinematicBody2D host, float delta)
        {
            host.MoveAndSlide(_dir * SPEED * delta);
            if(host.IsOnWall()) EmitSignal(nameof(Finished), "Staggered"); 
        }
    }
}

using Godot;
using System;
using System.Collections.Generic;

namespace Labyrinth.Objects.Player
{
    public class Entity : KinematicBody2D
    {
        [Export] public int speed = 80;
        private AnimationPlayer _animPlayer;
        private bool _isMoving = false;
        public Vector2 velocity = new Vector2();
        private Vector2 _playerToMouse = new Vector2();
        public PackedScene ScentScene = ResourceLoader.Load<PackedScene>("res://Objects/Player/Scent.tscn");
		public List<Scent> ScentTrail = new List<Scent>();
        public override void _Ready()
        {
            _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        }
        public override void _Process(float delta)
        {  
            _playerToMouse = GetGlobalMousePosition() - GlobalPosition;
            if (_isMoving) AnimateWalk();
            else AnimateIdle();
        }        
        public override void _PhysicsProcess(float delta)
        {
            velocity = Vector2.Zero;
            GetInput();                      
        }
        private void AnimateWalk()
        {        
            if (_playerToMouse.x > 0 && _playerToMouse.y > -5)  _animPlayer.Play("right");
            else if (_playerToMouse.x < 0 && _playerToMouse.y > -5)  _animPlayer.Play("left");
            else if (_playerToMouse.x > 0 && _playerToMouse.y <= -5) _animPlayer.Play("up_right");
            else if (_playerToMouse.x < 0 && _playerToMouse.y <= -5) _animPlayer.Play("up_left");
        }
        private void GetInput()
        {
            if (Input.IsActionPressed("ui_right"))
                velocity.x += 1;

            if (Input.IsActionPressed("ui_left"))
                velocity.x -= 1;

            if (Input.IsActionPressed("ui_down"))
                velocity.y += 1;

            if (Input.IsActionPressed("ui_up"))
                velocity.y -= 1;

            _isMoving = Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right") || Input.IsActionPressed("ui_up") || Input.IsActionPressed("ui_down");      
            velocity = velocity.Normalized() * speed;
            velocity = MoveAndSlide(velocity);               
        }
        private void AnimateIdle()
        {
            if (_playerToMouse.x > 0 && _playerToMouse.y > -5)  _animPlayer.Play("idle_right");
            else if (_playerToMouse.x < 0 && _playerToMouse.y > -5)  _animPlayer.Play("idle_left");
            else if (_playerToMouse.x > 0 && _playerToMouse.y <= -5) _animPlayer.Play("idle_up_right");
            else if (_playerToMouse.x < 0 && _playerToMouse.y <= -5) _animPlayer.Play("idle_up_left");
        }
        public void AddScent()
		{
			Scent scent = (Scent)ScentScene.Instance();
			scent.Position = Position;
			GetTree().Root.AddChild(scent);

			scent.Init(this);
			ScentTrail.Add(scent);
		}
    }
}

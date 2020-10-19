using Godot;
using System;

namespace Labyrinth.Entities.Player
{
    public class Player : KinematicBody2D
    {
        [Export] public int speed = 80;
        private AnimationPlayer _animPlayer;
        public Vector2 velocity = new Vector2();
        private Vector2 _playerToMouse = new Vector2();
        public override void _Ready()
        {
            _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        }
     
        public override void _Process(float delta)
        {  
            _playerToMouse = GetGlobalMousePosition() - GlobalPosition;
            if (_playerToMouse.x > 0 && _playerToMouse.y > -5) TurnRight();
            else if (_playerToMouse.x < 0 && _playerToMouse.y > -5) TurnLeft();
            else if (_playerToMouse.x > 0 && _playerToMouse.y <= -5) TurnUpRight();
            else if (_playerToMouse.x < 0 && _playerToMouse.y <= -5) TurnUpLeft();
        }
        
        public override void _PhysicsProcess(float delta)
        {
            velocity = new Vector2();

            GetInput();
                        
            velocity = velocity.Normalized() * speed;

            velocity = MoveAndSlide(velocity);
        }

        private void GetInput(){
            if (Input.IsActionPressed("ui_right"))
                velocity.x += 1;

            if (Input.IsActionPressed("ui_left"))
                velocity.x -= 1;

            if (Input.IsActionPressed("ui_down"))
                velocity.y += 1;

            if (Input.IsActionPressed("ui_up"))
                velocity.y -= 1;
        }

        private void TurnRight(){
            _animPlayer.Play("right");
        }
        private void TurnLeft(){
            _animPlayer.Play("left");
        }        
        private void TurnUpRight(){
            _animPlayer.Play("up_right");
        }
        private void TurnUpLeft(){
            _animPlayer.Play("up_left");
        }
    }
}

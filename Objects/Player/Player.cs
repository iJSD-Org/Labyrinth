using Godot;
using System;

namespace Labyrinth.Objects.Player
{
    public class Player : KinematicBody2D
    {
        [Export] public int speed = 80;
        private AnimationPlayer _animPlayer;
        private Sprite _torch;
        private bool _isMoving = false;
        public Vector2 velocity = new Vector2();
        private Vector2 _playerToMouse = new Vector2();
        public override void _Ready()
        {
            _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            _torch = GetNode<Sprite>("Torch");
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
            if (_playerToMouse.x > 0 && _playerToMouse.y > -5) {
                _animPlayer.Play("right");
                _torch.Position = GetNode<Position2D>("TorchRight").Position;
                _torch.FlipH = false;
                _torch.RotationDegrees = 25;
            }
            else if (_playerToMouse.x < 0 && _playerToMouse.y > -5) {
                _animPlayer.Play("left");
                _torch.Position = GetNode<Position2D>("TorchLeft").Position;
                _torch.FlipH = false;
                _torch.RotationDegrees = -25;
            }
            else if (_playerToMouse.x > 0 && _playerToMouse.y <= -5) {
                _animPlayer.Play("up_right");
                _torch.Position = GetNode<Position2D>("TorchRight").Position;
                _torch.FlipH = true;
                _torch.RotationDegrees = 25;
            } 
            else if (_playerToMouse.x < 0 && _playerToMouse.y <= -5) {
                _animPlayer.Play("up_left");
                _torch.Position = GetNode<Position2D>("TorchLeft").Position;
                _torch.FlipH = true;
                _torch.RotationDegrees = -25;
            }
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
            if (_playerToMouse.x > 0 && _playerToMouse.y > -5) {
                _animPlayer.Play("idle_right");
                _torch.Position = GetNode<Position2D>("TorchRight").Position;
                _torch.FlipH = false;
                _torch.RotationDegrees = 25;
            }
            else if (_playerToMouse.x < 0 && _playerToMouse.y > -5) {
                _animPlayer.Play("idle_left");
                _torch.Position = GetNode<Position2D>("TorchLeft").Position;
                _torch.FlipH = false;
                _torch.RotationDegrees = -25;
            }
            else if (_playerToMouse.x > 0 && _playerToMouse.y <= -5) {
                _animPlayer.Play("idle_up_right");
                _torch.Position = GetNode<Position2D>("TorchRight").Position;
                _torch.FlipH = true;
                _torch.RotationDegrees = 25;
            } 
            else if (_playerToMouse.x < 0 && _playerToMouse.y <= -5) {
                _animPlayer.Play("idle_up_left");
                _torch.Position = GetNode<Position2D>("TorchLeft").Position;
                _torch.FlipH = true;
                _torch.RotationDegrees = -25;
            }
        }
    }
}

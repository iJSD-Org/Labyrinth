using Godot;
using System;

namespace Labyrinth.Objects.Player.States
{
    public class Idle : State
    {
        private AnimationPlayer _animPlayer;
        private bool _isMoving;
        private Vector2 _playerToMouse;
        private Sprite _torch;
        private Position2D _torchRight;
        private Position2D _torchLeft;

        public override void Enter(KinematicBody2D host)
        { 
            _animPlayer = host.GetNode<AnimationPlayer>("AnimationPlayer");
            _torch = host.GetNode<Sprite>("Torch");
            _torchLeft = host.GetNode<Position2D>("TorchLeft");
            _torchRight = host.GetNode<Position2D>("TorchRight");
        }
        public override void Update(KinematicBody2D host, float delta)
        {
            _isMoving = Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right") || 
            Input.IsActionPressed("ui_up") || Input.IsActionPressed("ui_down"); 
            
            _playerToMouse = host.GetGlobalMousePosition() - host.GlobalPosition;
            AnimateIdle();
            if(_isMoving) EmitSignal(nameof(Finished), "Walk"); 
        }

        private void AnimateIdle()
        {
           if (_playerToMouse.x > 0 && _playerToMouse.y > -5) {
                _animPlayer.Play("idle_right");
                _torch.Position = _torchRight.Position;
                _torch.FlipH = false;
                _torch.RotationDegrees = 25;
            }
            else if (_playerToMouse.x < 0 && _playerToMouse.y > -5) {
                _animPlayer.Play("idle_left");
                _torch.Position = _torchLeft.Position;
                _torch.FlipH = true;
                _torch.RotationDegrees = -25;
            }
            else if (_playerToMouse.x > 0 && _playerToMouse.y <= -5) {
                _animPlayer.Play("idle_up_right");
                _torch.Position = _torchRight.Position;
                _torch.FlipH = false;
                _torch.RotationDegrees = 25;
            } 
            else if (_playerToMouse.x < 0 && _playerToMouse.y <= -5) {
                _animPlayer.Play("idle_up_left");
                _torch.Position = _torchLeft.Position;
                _torch.FlipH = true;
                _torch.RotationDegrees = -25;
            }
        }
    }
}

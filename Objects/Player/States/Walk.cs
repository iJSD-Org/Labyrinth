using Godot;
using Labyrinth.Objects;
using System;

namespace Labyrinth.Objects.Player.States
{
    public class Walk : State
    {
        [Export] public int Speed = 100;
        private AnimationPlayer _animPlayer;
        private bool _isMoving;
        private Vector2 _velocity;
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
            _playerToMouse = host.GetGlobalMousePosition() - host.GlobalPosition;
            _velocity = Vector2.Zero;
            GetInput();
            AnimateWalk();
            host.MoveAndSlide(_velocity);

            if(!_isMoving) EmitSignal(nameof(Finished), "Idle"); 
        }
        private void GetInput()
        {
            if (Input.IsActionPressed("ui_right"))
                _velocity.x += 1;

            if (Input.IsActionPressed("ui_left"))
                _velocity.x -= 1;

            if (Input.IsActionPressed("ui_down"))
                _velocity.y += 1;

            if (Input.IsActionPressed("ui_up"))
                _velocity.y -= 1;
            
            _isMoving = Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right") || 
            Input.IsActionPressed("ui_up") || Input.IsActionPressed("ui_down"); 
            _velocity = _velocity.Normalized() * Speed;              
        }

        private void AnimateWalk()
        {        
            if (_playerToMouse.x > 0 && _playerToMouse.y > -5) {
                _animPlayer.Play("right");
                _torch.Position = _torchRight.Position;
                _torch.FlipH = false;
                _torch.RotationDegrees = 25;
            }
            else if (_playerToMouse.x < 0 && _playerToMouse.y > -5) {
                _animPlayer.Play("left");
                _torch.Position = _torchLeft.Position;;
                _torch.FlipH = true;
                _torch.RotationDegrees = -25;
            }
            else if (_playerToMouse.x > 0 && _playerToMouse.y <= -5) {
                _animPlayer.Play("up_right");
                _torch.Position = _torchRight.Position;
                _torch.FlipH = false;
                _torch.RotationDegrees = 25;
            } 
            else if (_playerToMouse.x < 0 && _playerToMouse.y <= -5) {
                _animPlayer.Play("up_left");
                _torch.Position = _torchLeft.Position;
                _torch.FlipH = true;
                _torch.RotationDegrees = -25;
            }
        }

    }
}

using System;
using Godot;

namespace Labyrinth.Objects.Player.States
{
	public class Move : State
	{
		[Export] public int Speed = 100;

		public override void Update(KinematicBody2D host, float delta)
		{
			Vector2 inputDirection = new Vector2(
				Convert.ToInt32(Input.IsActionPressed("move_right")) - Convert.ToInt32(Input.IsActionPressed("move_left")),
				Convert.ToInt32(Input.IsActionPressed("move_down")) - Convert.ToInt32(Input.IsActionPressed("move_up")));

			if (inputDirection == Vector2.Zero)
			{
				EmitSignal(nameof(Finished), "Idle");
				return;
			}

			AnimateWalk(host);
			MoveObject(host, inputDirection);
		}

		public KinematicCollision2D MoveObject(KinematicBody2D host, Vector2 direction)
		{
			Vector2 velocity = direction.Normalized() * Speed;
			host.MoveAndSlide(velocity, Vector2.Zero);
			return host.GetSlideCount() == 0 ? null : host.GetSlideCollision(0);
		}

		private void AnimateWalk(KinematicBody2D host)
		{
			AnimationPlayer animPlayer = host.GetNode<AnimationPlayer>("AnimationPlayer");

			Sprite torch = host.GetNode<Sprite>("Torch");
			Position2D torchLeft = host.GetNode<Position2D>("TorchLeft");
			Position2D torchRight = host.GetNode<Position2D>("TorchRight");

			Vector2 playerToMouse = host.GetGlobalMousePosition() - host.GlobalPosition;

			if (playerToMouse.x > 0 && playerToMouse.y > -5)
			{
				animPlayer.Play("right");
				torch.Position = torchRight.Position;
				torch.FlipH = false;
				torch.RotationDegrees = 25;
			}
			else if (playerToMouse.x < 0 && playerToMouse.y > -5)
			{
				animPlayer.Play("left");
				torch.Position = torchLeft.Position; ;
				torch.FlipH = true;
				torch.RotationDegrees = -25;
			}
			else if (playerToMouse.x > 0 && playerToMouse.y <= -5)
			{
				animPlayer.Play("up_right");
				torch.Position = torchRight.Position;
				torch.FlipH = false;
				torch.RotationDegrees = 25;
			}
			else if (playerToMouse.x < 0 && playerToMouse.y <= -5)
			{
				animPlayer.Play("up_left");
				torch.Position = torchLeft.Position;
				torch.FlipH = true;
				torch.RotationDegrees = -25;
			}
		}
	}
}

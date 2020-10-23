using Godot;
using System;

namespace Labyrinth.Objects.Player.States
{
	public class Idle : State
	{
		public override void Update(KinematicBody2D host, float delta)
		{
			Vector2 inputDirection = new Vector2(
				Convert.ToInt32(Input.IsActionPressed("move_right")) - Convert.ToInt32(Input.IsActionPressed("move_left")),
				Convert.ToInt32(Input.IsActionPressed("move_down")) - Convert.ToInt32(Input.IsActionPressed("move_up")));

			if (inputDirection != Vector2.Zero)
				EmitSignal(nameof(Finished), "Move");

			AnimateIdle(host);
		}

		private void AnimateIdle(KinematicBody2D host)
		{
			AnimationPlayer animPlayer = host.GetNode<AnimationPlayer>("AnimationPlayer");
			Sprite torch = host.GetNode<Sprite>("Torch");
			Position2D torchLeft = host.GetNode<Position2D>("TorchLeft");
			Position2D torchRight = host.GetNode<Position2D>("TorchRight");

			Vector2 playerToMouse = host.GetGlobalMousePosition() - host.GlobalPosition;

			if (playerToMouse.x > 0 && playerToMouse.y > -5)
			{
				animPlayer.Play("idle_right");
				torch.Position = torchRight.Position;
				torch.FlipH = false;
				torch.RotationDegrees = 25;
			}
			else if (playerToMouse.x < 0 && playerToMouse.y > -5)
			{
				animPlayer.Play("idle_left");
				torch.Position = torchLeft.Position;
				torch.FlipH = true;
				torch.RotationDegrees = -25;
			}
			else if (playerToMouse.x > 0 && playerToMouse.y <= -5)
			{
				animPlayer.Play("idle_up_right");
				torch.Position = torchRight.Position;
				torch.FlipH = false;
				torch.RotationDegrees = 25;
			}
			else if (playerToMouse.x < 0 && playerToMouse.y <= -5)
			{
				animPlayer.Play("idle_up_left");
				torch.Position = torchLeft.Position;
				torch.FlipH = true;
				torch.RotationDegrees = -25;
			}
		}
	}
}

using Godot;

namespace Labyrinth.Objects.Player.States
{
	public class Idle : State
	{
		public override void Update(KinematicBody2D host, float delta)
		{
			bool isMoving = Input.IsActionPressed("move_left") || Input.IsActionPressed("move_right") ||
			Input.IsActionPressed("move_up") || Input.IsActionPressed("move_down");

			AnimateIdle(host);
			if (isMoving) EmitSignal(nameof(Finished), "Move");
		}

		private void AnimateIdle(KinematicBody2D host)
		{
			AnimationPlayer animPlayer = host.GetNode<AnimationPlayer>("AnimationPlayer");

			Position2D torch = host.GetNode<Sprite>("Torch").GetNode<Position2D>("CircleLight");
			Position2D torchLeft = host.GetNode<Position2D>("TorchLeft");
			Position2D torchRight = host.GetNode<Position2D>("TorchRight");

			Sprite torchSprite = host.GetNode<Sprite>("Torch");

			Vector2 playerToMouse = host.GetGlobalMousePosition() - host.GlobalPosition;

			if (playerToMouse.x > 0 && playerToMouse.y > -5)
			{
				animPlayer.Play("idle_right");
				torchSprite.Position = torchRight.Position;
				torch.RotationDegrees = 25;
			}
			else if (playerToMouse.x < 0 && playerToMouse.y > -5)
			{
				animPlayer.Play("idle_left");
				torchSprite.Position = torchLeft.Position;
				torch.RotationDegrees = -25;
			}
			else if (playerToMouse.x > 0 && playerToMouse.y <= -5)
			{
				animPlayer.Play("idle_up_right");
				torchSprite.Position = torchRight.Position;
				torch.RotationDegrees = 25;
			}
			else if (playerToMouse.x < 0 && playerToMouse.y <= -5)
			{
				animPlayer.Play("idle_up_left");
				torchSprite.Position = torchLeft.Position;
				torch.RotationDegrees = -25;
			}
		}
	}
}

using Godot;
using Labyrinth.Objects;
using Labyrinth.Objects.Player;

namespace Labyrinth.Levels
{
	public class WinScreen : Control
	{
		public override void _Ready()
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("fadein");
			GetNode("VBoxContainer").GetNode<Label>("ScoreLabel").Text = $"Time: {((Globals)GetNode("/root/Globals")).PlayerScore} seconds";
		}

		private void _on_Timer_timeout()
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("fadeout");
		}

		private void _on_AnimationPlayer_finished(string animation)
		{
			if (animation == "fadeout")
			{
				GetTree().ChangeScene("res://Levels/MainMenu.tscn");
			}
		}
	}
}

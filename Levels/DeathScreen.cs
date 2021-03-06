using Godot;
using Labyrinth.Objects;
using Labyrinth.Objects.Player;

namespace Labyrinth.Levels
{
	public class DeathScreen : Control
	{
		public override void _Ready()
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("FadeIn");
			GetNode("VBoxContainer").GetNode<Label>("ScoreLabel").Text = $"{((Globals)GetNode("/root/Globals")).PlayerScore} seconds...";
		}

		private void _on_Timer_timeout()
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("FadeOut");
		}

		private void _on_AnimationPlayer_animation_finished(string animation)
		{
			((Globals)GetNode("/root/Globals")).PlayerScore = 0;
			if (animation == "FadeOut") GetTree().ChangeScene("res://Levels/MainMenu.tscn");
		}
	}
}

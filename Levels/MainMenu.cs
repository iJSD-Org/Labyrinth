using Godot;
using Labyrinth.Objects;
using Labyrinth.Objects.Player;

namespace Labyrinth.Levels
{
	public class MainMenu : Control
	{
		public override void _Ready()
		{
			((Globals)GetNode("/root/Globals")).PlayerScore = 0;
			GetNode<AnimationPlayer>("AnimationPlayer").Play("Fade In");
		}

		private void _on_Quit_pressed()
		{
			GetTree().Quit();
		}

		private void _on_Start_pressed()
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("Fade Out");
		}

		private void _on_AnimationPlayer_animation_finished(string animName)
		{
			if (animName == "Fade Out") GetTree().ChangeScene("res://Levels/Labyrinth.tscn");
		}
	}
}

using Godot;

namespace Labyrinth.Levels
{
	public class MainMenu : Control
	{
		public override void _Ready()
		{
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
			if(animName == "Fade Out") GetTree().ChangeScene("res://Levels/Labyrinth.tscn");
		}
	}
}

using Godot;
using Labyrinth.Objects;
using Labyrinth.Objects.Player;

namespace Labyrinth.Levels
{
	public class MainMenu : Control
	{
		public override async void _Ready()
		{
			Globals globals = (Globals)GetNode("/root/Globals");
			globals.PlayerScore = 0;
			GetNode<AnimationPlayer>("AnimationPlayer").Play("Fade In");
			if (globals.DiscordTag is null)
			{

				OS.WindowMinimized = true;
				await globals.DiscordAuthAsync("http://localhost:8910/callback/");
				OS.WindowMaximized = true;
				OS.WindowFullscreen = true;
			}
			GetNode<Control>("Pause/Control").Visible = false;
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

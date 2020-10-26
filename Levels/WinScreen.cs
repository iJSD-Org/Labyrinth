using System;
using System.IO;
using System.Net;
using Godot;
using Labyrinth.Objects;

namespace Labyrinth.Levels
{
	public class WinScreen : Control
	{
		public override void _Ready()
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("fadein");
			GetNode("VBoxContainer").GetNode<Label>("ScoreLabel").Text = $"Time: {((Globals)GetNode("/root/Globals")).PlayerScore} seconds";
		}

		public override void _EnterTree()
		{
			AddScoreEntry(((Globals)GetNode("/root/Globals")).DiscordTag, ((Globals)GetNode("/root/Globals")).PlayerScore);
		}

		private void _on_Timer_timeout()
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("fadeout");
		}

		public void AddScoreEntry(string name, int score)
		{

			string url = $"http://dreamlo.com/lb/Pv6PwoSKi0e2o9TOfiZb-QuaU0x_d4VE2kmz0kXoVsqg/add/{Uri.EscapeDataString(name)}/{score}";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				using (Stream stream = response.GetResponseStream())
				{
					using (StreamReader reader = new StreamReader(stream ?? new MemoryStream()))
					{
						if (reader.ReadToEnd() == "OK")
						{
							Label scoreStatus = GetNode<Label>("ScoreStatus");
							scoreStatus.Text = "Score upload Success!";
							scoreStatus.AddColorOverride("font_color", Color.ColorN("Green"));
						}
						else
						{
							Label scoreStatus = GetNode<Label>("ScoreStatus");
							scoreStatus.Text = "Score upload fail!";
							scoreStatus.AddColorOverride("font_color", Color.ColorN("Red"));
						}
						GetNode<Label>("Prompt").Visible = true;
					}
				}
			}
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

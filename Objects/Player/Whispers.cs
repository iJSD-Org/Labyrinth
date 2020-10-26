using System;
using Godot;

namespace Labyrinth.Objects.Player
{
	public class Whispers : Node
	{
		private readonly Random _random = new Random();

		private void _on_WhisperTimer_timeout()
		{

			GetNode<Timer>("WhisperTimer").Stop();
			GetNode<Timer>("WhisperTimer").WaitTime = _random.Next(30, 110);
			GetNode<AudioStreamPlayer2D>($"Whisper{_random.Next(1, 4)}").Play();
			GetNode<Timer>("WhisperTimer").Start();
		}
	}
}
using System;
using Godot;
using Labyrinth.Objects;
using Entity = Labyrinth.Objects.Enemies.Minotaur.Entity;

namespace Labyrinth.Levels
{
	public class Labyrinth : Node
	{
		private readonly Random _random = new Random();

		private readonly PackedScene _minotaur = ResourceLoader.Load<PackedScene>("res://Objects/Enemies/Minotaur/Minotaur.tscn");

		public override void _Ready()
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("Fade In");
			GetNode<KinematicBody2D>("Node2D/Player").Position = GetNode<Position2D>($"SpawnPoints/{_random.Next(1,5)}").Position;
		}

		private void _on_MinotaurSpawnTimer_timeout()
		{
			if (GetTree().Root.GetNode<Node2D>("Scent") != null && GetNode<KinematicBody2D>("Node2D/Minotaur") == null)
			{
				Entity minotaurInstance = (Entity)_minotaur.Instance();
				minotaurInstance.Position = GetTree().Root.GetNode<Node2D>("Scent").Position;
				GetNode<Node2D>("Node2D").AddChild(minotaurInstance);
			}
			GetNode<Timer>("MinotaurSpawnTimer").Stop();
			GetNode<Timer>("MinotaurSpawnTimer").WaitTime = (float)(_random.NextDouble() * (110 - 40) + 40);
			GetNode<Timer>("MinotaurSpawnTimer").Start();
		}

		private void _on_Exit_body_entered(KinematicBody2D body)
		{
			if (body.IsInGroup("player"))
			{
				GetTree().ChangeScene("res://Levels/WinScreen.tscn");
			}
		}
		private void _on_ScoreTimer_timeout()
		{
			//Score is in seconds
			((Globals)GetNode("/root/Globals")).PlayerScore += 1;
		}
	}
}

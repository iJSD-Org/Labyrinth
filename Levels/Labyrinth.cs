using Godot;
using Labyrinth.Objects.Enemies.Minotaur;
using Labyrinth.Objects.Player.Globals;
using System;

namespace Labyrinth.Levels.Arena
{
    public class Labyrinth : Node
    {
        private Random _random = new Random();

        private PackedScene _minotaur = (PackedScene)ResourceLoader.Load("res://Objects/Enemies/Minotaur/Minotaur.tscn"); 
        public override void _Ready()
        {
            GetNode<AnimationPlayer>("AnimationPlayer").Play("Fade In");
        }

        private void _on_MinotaurSpawnTimer_timeout()
        {
            if(GetTree().Root.GetNode<Node2D>("Scent") != null && GetNode<KinematicBody2D>("Node2D/Minotaur") == null) 
            {
                 Entity minotaurInstance = (Entity)_minotaur.Instance();
                 minotaurInstance.Position = GetTree().Root.GetNode<Node2D>("Scent").Position;
                 GetNode<Node2D>("Node2D").AddChild(minotaurInstance);
            }
            GetNode<Timer>("MinotaurSpawnTimer").Stop();
            GetNode<Timer>("MinotaurSpawnTimer").WaitTime = (float)(_random.NextDouble() * (110 - 40) + 40);
            GetNode<Timer>("MinotaurSpawnTimer").Start();
        }

        private void _on_ScoreTimer_timeout()
        {
            //Score is in seconds
            Globals.PlayerScore += 1;
        }
    }
}
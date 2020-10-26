using Godot;
using Labyrinth.Objects.Player.Globals;
using System;

namespace Labyrinth.Levels
{
    public class WinScreen : Control
    {
        public override void _Ready()
        {
            GetNode<AnimationPlayer>("AnimationPlayer").Play("fadein");
            GetNode("VBoxContainer").GetNode<Label>("ScoreLabel").Text = $"Time: {Globals.PlayerScore} seconds";
        }

        private void _on_Timer_timeout()
        {
            GetNode<AnimationPlayer>("AnimationPlayer").Play("fadeout");
        }

        private void _on_AnimationPlayer_finished(string animation)
        {
            if(animation == "fadeout")
            {
                GetTree().ChangeScene("res://Levels/MainMenu.tscn");
            }
        }
    }
}
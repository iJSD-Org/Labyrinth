using Godot;
using Labyrinth.Objects.Player.Globals;

namespace Labyrinth.Levels
{
    public class DeathScreen : Node2D
    {
        public override void _Ready()
        {
            GetNode<AnimationPlayer>("AnimationPlayer").Play("FadeIn");
            GetNode<Label>("ScoreLabel").Text = $"{Globals.PlayerScore} seconds...";
        }

        private void _on_Timer_timeout()
        {
            GetNode<AnimationPlayer>("AnimationPlayer").Play("FadeOut");
        }

        private void _on_AnimationPlayer_animation_finished(string animation)
        {
            Globals.PlayerScore = 0;
            if (animation == "FadeOut") GetTree().ChangeScene("res://Levels/MainMenu.tscn");
        }
    }   
}

using Godot;
using System;

namespace Labyrinth.Levels
{
    public class Intro : Control
    {
        private int _scene = 0;
        public override void _Ready()
        {
            GetNode<AnimationPlayer>("AnimationPlayer").Play("fadein");
            GetNode<Timer>("Timer").Start();
        }

        private void _on_Timer_timeout()
        {
            GetNode<Timer>("Timer").Stop();
            GetNode<AnimationPlayer>("AnimationPlayer").Play("fadeout");
        }

        private void _on_AnimationPlayer_finished(string animation)
        {
            if (animation == "fadeout")
            {
                if (_scene == 0) GetNode<Control>("Logo").Hide();
                else if (_scene == 1) GetNode<Label>("Text").Show();
                else GetTree().ChangeScene("res://Levels/MainMenu.tscn");
                _scene++;
                GetNode<Timer>("Timer").Start();
            }
           
        }

    }
}

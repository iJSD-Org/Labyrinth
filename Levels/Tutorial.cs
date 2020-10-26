using Godot;
using System;

namespace Labyrinth.Levels
{
    public class Tutorial : Control
    {
        private int _scene = 1;
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
            if (animation == "fadeout" && _scene != 7)
            {
                if (_scene == 1) 
                {
                    GetNode<Control>("Logo").Hide();
                }
                GetNode<Label>($"Text/{_scene}").Hide();
                _scene++;
                GetNode<Label>($"Text/{_scene}").Show();
                GetNode<AnimationPlayer>("AnimationPlayer").Play("fadein");
                GetNode<Timer>("Timer").Start();
            }

            else if(_scene == 7) GetTree().ChangeScene("res://Levels/MainMenu.tscn");
        }
    }
}
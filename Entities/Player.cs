using Godot;
using System;

namespace Labyrinth.Entities.Player
{
    public class Player : KinematicBody2D
    {
        private AnimationPlayer _animPlayer;
        public override void _Ready()
        {
            _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer"); 
        }
     
        public override void _Process(float delta)
        {  

        }

        private void PlayAnimation()
        {
       
        }
    }
}

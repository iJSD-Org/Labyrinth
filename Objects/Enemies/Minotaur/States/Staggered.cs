using Godot;
using System;
using Labyrinth.Objects;

namespace Labyrinth.Objects.Enemies.Minotaur.States
{
    public class Staggered : State
    {
        public override void _Ready()
        {
        
        }
        public override void Enter(KinematicBody2D host)
        {
            GetNode<Timer>("StaggerTimer").Start();
        }
        public override void Exit(KinematicBody2D host)
        {

        }
        public override void HandleInput(KinematicBody2D host, InputEvent @event)
        {

        }
        public override void Update(KinematicBody2D host, float delta)
        {
        
        }

        private void _on_StaggerTimer_timeout()
        {
            GetNode<Timer>("StaggerTimer").Stop();
            EmitSignal(nameof(Finished), "Chase");
        }
    }
}

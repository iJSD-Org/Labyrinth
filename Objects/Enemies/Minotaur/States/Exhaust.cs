using Godot;

namespace Labyrinth.Objects.Enemies.Minotaur.States
{
    public class Exhaust : State
    {
	    public override void Enter(KinematicBody2D host)
        {
            GetNode<Timer>("ExhaustTimer").Start();
        }

        private void _on_StaggerTimer_timeout()
        {
            GetNode<Timer>("ExhaustTimer").Stop();
            EmitSignal(nameof(Finished), "Chase");
        }
    }
}

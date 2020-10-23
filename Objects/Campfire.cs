using Godot;

namespace Labyrinth.Objects
{
	public class Campfire : KinematicBody2D
	{
		public override void _Ready()
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("burn");
		}
	}
}

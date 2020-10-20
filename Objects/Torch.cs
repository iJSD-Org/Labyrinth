using Godot;

namespace Labyrinth.Objects
{
	public class Torch : Sprite
	{
		public override void _Ready()
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("burn");
		}
	}
}

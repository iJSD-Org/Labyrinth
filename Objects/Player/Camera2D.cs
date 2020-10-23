using Godot;

namespace Labyrinth.Objects.Player
{
    public class Camera2D : Godot.Camera2D
    {
        public override void _Ready()
        {
            SetAsToplevel(true);
		    SetProcess(false);
        }

        public override void _PhysicsProcess(float delta)
        {
            Position = new Vector2(
				    -((GetParent<KinematicBody2D>().Position.x - GetGlobalMousePosition().x) / 2 * .6f) + GetParent<KinematicBody2D>().GlobalPosition.x, 
				    -((GetParent<KinematicBody2D>().Position.y - GetGlobalMousePosition().y) / 2 * .6f) + GetParent<KinematicBody2D>().GlobalPosition.y);
        }
    }
}
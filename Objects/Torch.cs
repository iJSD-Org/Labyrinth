using Godot;

namespace Labyrinth.Objects
{
	public class Torch : Sprite
	{
		public override void _Ready()
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("burn");
		}
		
		public override void _Process(float delta)
    	{
        	GetNode<Position2D>("PointLight").Rotation = GetParent<KinematicBody2D>().GetAngleTo(GetGlobalMousePosition()) + 20.4f;
    	}

    	public override void _PhysicsProcess(float delta)
    	{
       		if(Input.IsActionPressed("Point"))
			{
				GetNode<Position2D>("CircleLight").Visible = false;
				GetNode<Position2D>("PointLight").Visible = true;
				GetNode<Position2D>("PointLight").Scale = new Vector2(1, 1);
			}
			else 
			{
				GetNode<Position2D>("CircleLight").Visible = true;
				GetNode<Position2D>("PointLight").Visible = false;
				GetNode<Position2D>("PointLight").Scale = new Vector2(0.001f, 0.001f);
			}
		}
	}
}

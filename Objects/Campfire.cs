using Godot;
using System;

public class Campfire : KinematicBody2D
{
    public override void _Ready()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("burn");
    }
}

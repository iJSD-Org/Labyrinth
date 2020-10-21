using Godot;
using System;

public class Fountain : KinematicBody2D
{
    public override void _Ready()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("water");   
    }
}

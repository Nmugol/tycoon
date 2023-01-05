using Godot;
using System;

public class Play : Button
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    void _on_Play_pressed()
    {
        GetTree().ChangeScene("res://Scenes/Save/Save_and_load.tscn");
    }
}

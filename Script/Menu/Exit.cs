using Godot;
using System;

public class Exit : Button
{    void _on_Exit_pressed()
    {
        GetTree().Quit();
    }
}

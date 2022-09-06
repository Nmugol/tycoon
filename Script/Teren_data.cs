using Godot;
using System;

public class Teren_data : Node2D
{
    [Export] public int MapX = 25;
    [Export] public int MapY = 25;
    public override void _Ready()
    {
        
    }

    public void _SetMapData(ref int sizex, ref int sizey)
    {
        sizex = MapX;
        sizey = MapY;
    }
}

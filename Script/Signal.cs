using Godot;
using System;

public class Signal : Node
{
    [Signal] public delegate void SetMountain(int posx,int posy);
    [Signal] public delegate void StartGenarate(int x_size, int y_size);
}

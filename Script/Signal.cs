using Godot;
using System;

public class Signal : Node
{
    [Signal] public delegate void SetMountain(int posx,int posy);
}

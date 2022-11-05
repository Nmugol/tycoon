using Godot;
using System;

public class Signal : Node
{
    [Signal] public delegate void SetMountain(int posx,int posy);
    [Signal] public delegate void StartGenarate(int x_size, int y_size);
    [Signal] public delegate void CheckedGround(int posx, int posy);
    [Signal] public delegate void CheckedMountein(int posx, int posy);
    [Signal] public delegate void GroundIsFree();
    [Signal] public delegate void MounteinIsFree();
}

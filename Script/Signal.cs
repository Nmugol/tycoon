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
    [Signal] public delegate void YesHaveResorces();
    [Signal] public delegate void NoHaveResorces();
    [Signal] public delegate void HaveResorces(string resorce, int how_many);
    [Signal] public delegate void SetTimer(string resorce, int time_dilay, int how_mady_add);
    [Signal] public delegate void Add_Resorses(string resorce, int how_many_add);
}

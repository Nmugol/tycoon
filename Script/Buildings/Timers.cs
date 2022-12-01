using Godot;
using System;

public class Timers : Node
{
    private Signal signalcs;
    [Export] private NodePath Parent_path;

    private string resorce_to_add;
    private int how_many;
    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        signalcs.Connect("SetTimer",this,"_SetTimer");
    }

    private void _SetTimer(string resorce, int time_dilay, int how_mady_add)
    {
        resorce_to_add = resorce;
        how_many = how_mady_add;


        Timer factory = new Timer();
        factory.WaitTime = time_dilay;
        factory.OneShot=false;
        factory.Connect("timeout",this,"_Add_Resorces");

        Node Parent = GetNode<Node>(Parent_path);
        Parent.AddChild(factory);

        factory.Start();
    }

    private void _Add_Resorces()
    {
        signalcs.EmitSignal(nameof(Signal.Add_Resorses),resorce_to_add,how_many);
    }
}

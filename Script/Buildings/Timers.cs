using Godot;
using System;
using System.Collections.Generic;

public class Timers : Node
{
    private Signal signalcs;
    private SaveData savedata;
    [Export] private NodePath Parent_path;

    private string resorce_to_add;
    private int how_many;
    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        savedata = GetNode<SaveData>("/root/SaveData");
        signalcs.Connect("SetTimer",this,"_SetTimer");

        File save = new File();
        string path = "user://"+savedata.save_file+".json";

        if(save.FileExists(path))
        {
            foreach(KeyValuePair<string,int[]> key in savedata.factory)
            {
                int[] table = key.Value;
                int how_many = table[0];
                int type = table[1];

                for(int i = 0; i<how_many; i++)
                {
                    switch(type)
                    {
                        case 0:
                            _SetTimer("stone",10,15);
                            GD.Print("p");
                        break;
                    }
                }

            }
        }
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

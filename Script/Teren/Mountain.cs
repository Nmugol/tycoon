using Godot;
using System;
using System.Collections.Generic;

public class Mountain : TileMap
{
    private int map_size_x;
    private int map_size_y;

    private Signal signalcs;
    private Save_bluprint save_file;

    private SaveData savedata;

    int size;

    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        save_file = GetNode<Save_bluprint>("/root/SaveBluprint");
        savedata = GetNode<SaveData>("/root/SaveData");

        signalcs.Connect("CheckedMountein",this,"_CheckedMountein");

        size = savedata.moon_size;

        File save = new File();
        string path = "user://"+savedata.save_file+".json";

        if(save.FileExists(path)) _LoadMap();
        else _StartGenarate();
    }

    private void _StartGenarate()
    {
        Random rng = new Random();

        int size = savedata.moon_size;
        
        int[,] tail_tab = new int[size,size];

        int offsetx=1;
        int offsety=0;
        if(size == 20) offsetx += size/2;
        if(size == 25) offsetx += size/4;
        if(size == 30)
        {
            offsetx = -3;
            offsety = -6; 
        }
        for(int i=size+offsetx; i<(2*size)+offsetx; i++)
        {
            for(int j=0+offsety; j<size+offsety; j++)
            {
                int procen = rng.Next(13);

                switch(procen)
                {
                    case 0: case 1://Mountain
                        SetCell(i,j,0);
                        tail_tab[i-(size+offsetx),j-offsety]=0;
                        signalcs.EmitSignal(nameof(Signal.SetMountain),i,j);
                    break;

                    default://void
                        SetCell(i,j,-1);
                        tail_tab[i-(size+offsetx),j-offsety]=-1;
                    break;
                }
            }
        }

        savedata.mountains = tail_tab;
    }
    private void _CheckedMountein(int x, int y)
    {
        if(GetCell(x,y)!=0)
        {
            signalcs.EmitSignal(nameof(Signal.MounteinIsFree));
        }
    }

    void _LoadMap()
    {
        int offsetx=1;
        int offsety=0;
        if(size == 20) offsetx += size/2;
        if(size == 25) offsetx += size/4;
        if(size == 30)
        {
            offsetx = -3;
            offsety = -6; 
        }
        
        for(int i=0; i<size; i++)
        {
            for(int j=0; j<size; j++)
            {
                int procen = savedata.mountains[i,j];

                switch(procen)
                {
                    case 0://wather
                        SetCell(i+offsetx+size,j+offsety,0);
                    break;
                    
                    default://ground
                        SetCell(i+offsetx+size,j+offsety,-1);
                    break;
                }
            }
        }
    }
}

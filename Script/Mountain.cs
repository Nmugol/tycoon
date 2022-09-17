using Godot;
using System;

public class Mountain : TileMap
{
    private int map_size_x;
    private int map_size_y;

    private Signal signalcs;

    private Random rand = new Random();
    private Teren_data terenData = new Teren_data();

    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        signalcs.Connect("StartGenarate",this,"_GenTeren");
    }

    private void _GenTeren(int x_size, int y_size)
    {
        map_size_x = x_size;
        map_size_y = y_size;

        _CleraMap(map_size_x,map_size_y);

        for(int i=(map_size_x+1); i<((2*map_size_x)+1); i++)
        {
            for(int j=0; j<map_size_y; j++)
            {
                int procen = rand.Next(13);

                switch(procen)
                {
                    case 0: case 1://Mountain
                        SetCell(i,j,0);
                        signalcs.EmitSignal(nameof(Signal.SetMountain),i,j);
                    break;

                    default://void
                        SetCell(i,j,-1);
                    break;
                }
            }
        }
    }

    private void _CleraMap(int size_x, int size_y)
    {

        for(int i=-1; i<1000; i++)
        {
            for(int j=-1; j<1000; j++)
            {
                SetCell(i,j,-1);
            }
        } 
    }
}

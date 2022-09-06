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

        terenData._SetMapData(ref map_size_x, ref map_size_y);

        int offset = map_size_x+1;
        _GenTeren(offset);
    }

    private void _GenTeren(int offset)
    {
        for(int i=offset; i<map_size_x+offset; i++)
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
                }
            }
        }
    }
}

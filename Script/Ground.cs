using Godot;
using System;

public class Ground : TileMap
{
    private int map_size_x;
    private int map_size_y;

    private Signal signalcs;

    private Random rand = new Random();
    private Teren_data terenData = new Teren_data();

    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        signalcs.Connect("SetMountain",this,"_SetGround");

        terenData._SetMapData(ref map_size_x, ref map_size_y);

        int offset = map_size_x+1;

        _GenTeren(offset);
        _FindCellToRelpace(offset);
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
                    case 0: case 1: //water
                        SetCell(i,j,2);
                    break;

                    case 2: case 3://hole
                        SetCell(i,j,0);
                    break;

                    default://ground
                        SetCell(i,j,1);
                    break;
                }
            }
        }
    }

    // Find a water call on map
    private void _FindCellToRelpace( int offset)
    {
        for(int i=offset; i<map_size_x+offset; i++)
        {
            for(int j=0; j<map_size_y; j++)
            {
                var cellid = GetCell(i,j);
                if(cellid==2)
                {
                    _ReplaceCell(i,j);
                }
            }
        }         
    }

    private void _ReplaceCell(int x, int y)
    {

        // Get a typo of cell aroun water cell
        var cell1 = GetCell(x-1,y);
        var cell2 = GetCell(x,y-1);
        var cell3 = GetCell(x+1,y);
        var cell4 = GetCell(x,y+1);
        
        // If cell aroun is hole replpac them water cell
        if(cell1==0)
        {
            SetCell(x-1,y,2);
            _ReplaceCell(x-1,y);
        }
        
        if(cell2==0)
        {
            SetCell(x,y-1,2);
            _ReplaceCell(x,y-1);
        }
        
        if(cell3==0)
        {
            SetCell(x+1,y,2);
            _ReplaceCell(x+1,y);
        }
        
        if(cell4==0)
        {
            SetCell(x,y+1,2);
            _ReplaceCell(x,y+1);
        }

        // If water cell is around moon cell replace it moon cell
        if(cell1==1 && cell2==1 && cell3==1 && cell4==1)
        {
            SetCell(x,y,1);
        }
    }

    void _SetGround(int x, int y)
    {
        SetCell(x,y,1);
    }
}

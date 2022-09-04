using Godot;
using System;

public class teren : TileMap
{
    [Export] private int map_size_x = 7;
    [Export] private int map_size_y = 7;

    private Random rand = new Random();

    public override void _Ready()
    {
        int offset = map_size_x+1;

        var tileId = TileSet.FindTileByName("Ground");

        _GenTeren(offset);
        _FindCellToRelpace(offset);
    }

    private void _GenTeren(int offset)
    {
        for(int i=offset; i<map_size_x+offset; i++)
        {
            for(int j=0; j<map_size_y; j++)
            {
                int procen = rand.Next(10);

                switch(procen)
                {
                    case 0: case 1://water
                        SetCell(i,j,2);
                    break;

                    case 2://mountin
                        SetCell(i,j,4);
                    break;

                    case 3: case 4://hole
                        SetCell(i,j,3);
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
        if(cell1==3)
        {
            SetCell(x-1,y,2);
            _ReplaceCell(x-1,y);
        }
        
        if(cell2==3)
        {
            SetCell(x,y-1,2);
            _ReplaceCell(x,y-1);
        }
        
        if(cell3==3)
        {
            SetCell(x+1,y,2);
            _ReplaceCell(x+1,y);
        }
        
        if(cell4==3)
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
}

using Godot;
using System;
using System.Collections.Generic;

public class Ground : TileMap
{
    private Signal signalcs;
    private Save_bluprint save_file;
    private SaveData savedata;

    int size;

    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        save_file = GetNode<Save_bluprint>("/root/SaveBluprint");
        savedata = GetNode<SaveData>("/root/SaveData");

        signalcs.Connect("SetMountain",this,"_SetGround");
        signalcs.Connect("CheckedGround",this,"_CheckedGround");

        size = savedata.moon_size;
        int[,] tail_tab = new int[size,size];

        File save = new File();
        string path = "user://"+savedata.save_file+".json";

        if(save.FileExists(path)) _LoadMap();
        else _StartGenarate(tail_tab);
    }
    
    private void _StartGenarate(int[,]tail_tab)
    {
        _ClearMap();
        Random rng = new Random();
        
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
                    case 0: case 1: //water
                        SetCell(i,j,2);
                        tail_tab[i-(size+offsetx),j-offsety]=2;
                    break;

                    case 2: case 3://hole
                        SetCell(i,j,0);
                        tail_tab[i-(size+offsetx),j-offsety]=0;
                    break;

                    default://ground
                        SetCell(i,j,1);
                        tail_tab[i-(size+offsetx),j-offsety]=1;
                    break;
                }
            }
        }
        _FindCellToRelpace(tail_tab);
        savedata.ground=tail_tab;
    }

    // Find a water call on map
    private void _FindCellToRelpace(int[,]tail_tab)
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
        
        for(int i=size+offsetx; i<(2*size)+offsetx; i++)
        {
            for(int j=0+offsety; j<size+offsety; j++)
            {
                var cellid = GetCell(i,j);
                if(cellid==2)
                {
                    _ReplaceCell(i,j,tail_tab);
                }
            }
        }         
    }

    private void _ReplaceCell(int x, int y, int[,]tail_tab)
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
    

        // Get a typo of cell aroun water cell
        var cell1 = GetCell(x-1,y);
        var cell2 = GetCell(x,y-1);
        var cell3 = GetCell(x+1,y);
        var cell4 = GetCell(x,y+1);
        
        // If cell aroun is hole replpac them water cell
        if(cell1==0)
        {
            SetCell(x-1,y,2);
            tail_tab[x-(size+offsetx)-1,y-offsety]=2;
            _ReplaceCell(x-1,y,tail_tab);
        }
        
        if(cell2==0)
        {
            SetCell(x,y-1,2);
            tail_tab[x-(size+offsetx),y-offsety-1]=2;
            _ReplaceCell(x,y-1,tail_tab);
        }
        
        if(cell3==0)
        {
            SetCell(x+1,y,2);
            tail_tab[x-(size+offsetx)+1,y-offsety]=2;
            _ReplaceCell(x+1,y,tail_tab);
        }
        
        if(cell4==0)
        {
            SetCell(x,y+1,2);
            tail_tab[x-(size+offsetx),y-offsety+1]=2;
            _ReplaceCell(x,y+1,tail_tab);
        }

        // If water cell is around moon cell replace it moon cell
        if(cell1==1 && cell2==1 && cell3==1 && cell4==1)
        {
            SetCell(x,y,1);
            tail_tab[x-(size+offsetx),y-offsety]=1;
        }
    }

    //ste groun cels under moutein
    private void _SetGround(int x, int y)
    {
        SetCell(x,y,1);
    }

    //rmowe all cells on map
    private void _ClearMap()
    {
        for(int i=-1; i<1000; i++)
        {
            for(int j=-1; j<1000; j++)
            {
                SetCell(i,j,-1);
            }
        } 
    }

    private void _CheckedGround(int x,int y)
    {
        if(GetCell(x,y)==1)
        {
            signalcs.EmitSignal(nameof(Signal.GroundIsFree));
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
                int procen = savedata.ground[i,j];

                switch(procen)
                {
                    case 0://wather
                        SetCell(i+offsetx+size,j+offsety,0);
                    break;

                    case 2://kole
                        SetCell(i+offsetx+size,j+offsety,2);
                    break;

                    default://ground
                        SetCell(i+offsetx+size,j+offsety,1);
                    break;
                }
            }
        }
    }
}

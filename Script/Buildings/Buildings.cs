using Godot;
using System;
using System.Collections.Generic;


public class Buildings : TileMap
{
    private Signal signalcs;
    private Save_bluprint save_file;
    private SaveData savedata;

    //value to colect build type 
    private int factory_type=-1;

    //help flag to seting building
    private bool groun_is_free=false;
    private bool mountaun_is_free=false;
    private bool have_resorces=false;

    int[,] map;
    int size;

    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        save_file = GetNode<Save_bluprint>("/root/SaveBluprint");
        savedata = GetNode<SaveData>("/root/SaveData");

        signalcs.Connect("GroundIsFree",this,"_GroundIsFree");
        signalcs.Connect("MounteinIsFree",this,"_MounteinIsFree");
        signalcs.Connect("YesHaveResorces",this,"_YesHaveResorces");
        signalcs.Connect("NoHaveResorces",this,"_NoHaveResorces");

        size = savedata.moon_size;
        int[,] tail_tab = new int[size,size];


        File save = new File();
        string path = "user://"+savedata.save_file+".json";

        if(save.FileExists(path)) _LoadMap();
        else
        {
            for(int i=0; i<size; i++)
            {
                for(int j=0; j<size; j++)
                {
                    tail_tab[i,j]=factory_type;
                }
            }

            map=tail_tab;
            savedata.buildings=map;
        }
        map = savedata.buildings;
    }

    public override void _Process(float delta)
    {
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

        //get mous position and conwerst to cell position
        var mouse_pos = WorldToMap(GetGlobalMousePosition());

        //convert cell cortynar to int value and separat x coed and y code 
        int cell_pos_x = mouse_pos.x.ToString().ToInt();
        int cell_pos_y = mouse_pos.y.ToString().ToInt();

        if(factory_type!=-1)
        {
            if(Input.IsActionJustPressed("lmb"))
            {
                //if palyer whone to build factory then cheked them groun is free ando don't hawen't mountein on this cell
                switch(factory_type)
                {
                    case 0://stonefactory
                        signalcs.EmitSignal(nameof(Signal.CheckedGround),cell_pos_x,cell_pos_y);
                        signalcs.EmitSignal(nameof(Signal.CheckedMountein),cell_pos_x,cell_pos_y);
                        signalcs.EmitSignal(nameof(Signal.HaveResorces),"stone", 50, groun_is_free, mountaun_is_free);

                        if(groun_is_free && mountaun_is_free && have_resorces)
                        {
                            SetCell(cell_pos_x,cell_pos_y,factory_type);
                            map[cell_pos_x-(size+offsetx),cell_pos_y-offsety]=factory_type;
                            signalcs.EmitSignal(nameof(Signal.SetTimer),"stone",10,15);
                            int[] temp = savedata.factory["stonfactory"];
                            temp[0]+=1;
                            savedata.factory["stonfactory"]=temp;
                        }
                    break;
                }
                _Reset();
            }
        }
        
    }
    private void _on_Factory_pressed()
    {
        factory_type=0;
    }
    private void _GroundIsFree()
    {
        groun_is_free=true;
    }
    private void _MounteinIsFree()
    {
        mountaun_is_free=true;
    }

    private void _YesHaveResorces()
    {
        have_resorces=true;
    }

    private void _NoHaveResorces()
    {
        have_resorces=false;
    }

    //restet all value usin to bild buildings
    private void _Reset()
    {
        factory_type=-1;
        groun_is_free=false;
        mountaun_is_free=false;
        have_resorces=false;
    }

    void _on_Button_pressed()
    {
        signalcs.EmitSignal(nameof(Signal.SaveData));
        GetTree().ChangeScene("res://Scenes/Menu/Main_menu.tscn");
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
                int procen = savedata.buildings[i,j];

                switch(procen)
                {
                    case 0://ston factory
                        SetCell(i+offsetx+size,j+offsety,0);
                    break;

                    default://empti
                        SetCell(i+offsetx+size,j+offsety,-1);
                    break;
                }
            }
        }
    }
}

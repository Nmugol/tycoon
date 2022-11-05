using Godot;
using System;

public class Buildings : TileMap
{
    private Signal signalcs;

    //value to colect build type 
    private int factory_type=-1;

    //help flag to seting building
    private bool groun_is_free=false;
    private bool mountaun_is_free=false;

    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        signalcs.Connect("GroundIsFree",this,"_GroundIsFree");
        signalcs.Connect("MounteinIsFree",this,"_MounteinIsFree");
    }

    public override void _Process(float delta)
    {

        //get mous position and conwerst to cell position
        var mouse_pos = WorldToMap(GetGlobalMousePosition());

        //convert cell cortynar to int value and separat x coed and y code 
        int cell_pos_x = mouse_pos.x.ToString().ToInt();
        int cell_pos_y = mouse_pos.y.ToString().ToInt();

        if(Input.IsActionJustPressed("lmb"))
        {
            //if palyer whone to build factory then cheked them groun is free ando don't hawen't mountein on this cell
            if(factory_type!=-1)
            {
                signalcs.EmitSignal(nameof(Signal.CheckedGround),cell_pos_x,cell_pos_y);
                signalcs.EmitSignal(nameof(Signal.CheckedMountein),cell_pos_x,cell_pos_y);

                if(groun_is_free==true && mountaun_is_free==true)
                {
                    SetCell(cell_pos_x,cell_pos_y,factory_type);
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

    //restet all value usin to bild buildings
    private void _Reset()
    {
        factory_type=-1;
        groun_is_free=false;
        mountaun_is_free=false;
    }
}

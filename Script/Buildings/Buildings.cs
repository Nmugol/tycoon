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
    private bool have_resorces=false;

    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        signalcs.Connect("GroundIsFree",this,"_GroundIsFree");
        signalcs.Connect("MounteinIsFree",this,"_MounteinIsFree");
        signalcs.Connect("YesHaveResorces",this,"_YesHaveResorces");
        signalcs.Connect("NoHaveResorces",this,"_NoHaveResorces");
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
            signalcs.EmitSignal(nameof(Signal.CheckedGround),cell_pos_x,cell_pos_y);
            signalcs.EmitSignal(nameof(Signal.CheckedMountein),cell_pos_x,cell_pos_y);
            //if palyer whone to build factory then cheked them groun is free ando don't hawen't mountein on this cell
            switch(factory_type)
            {
                case 0:
                    signalcs.EmitSignal(nameof(Signal.HaveResorces),"stone", 50);

                    if(groun_is_free && mountaun_is_free && have_resorces)
                    {
                        SetCell(cell_pos_x,cell_pos_y,factory_type);
                        signalcs.EmitSignal(nameof(Signal.SetTimer),"stone",10,15);
                    }
                break;
                
            }
            _Reset();
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
}

using Godot;
using System;
using System.Collections.Generic;

public class Generate : Button
{   
    [Export] private NodePath moon_name_path;
    [Export] private NodePath moon_size_path;
    private Signal signalcs;
    private Save_bluprint save_file;
    private SaveData savedata;
    

    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        save_file = GetNode<Save_bluprint>("/root/SaveBluprint");
        savedata = GetNode<SaveData>("/root/SaveData");
    }
    void _on_Generate_pressed()
    {
        TextEdit moon_name = GetNode<TextEdit>(moon_name_path);
        Slider moon_size = GetNode<Slider>(moon_size_path);

        if(moon_name.Text!=null) savedata.moon_name=moon_name.Text;
        if(moon_size!=null) savedata.moon_size = (int)moon_size.Value;
        
        GetTree().ChangeScene("res://Scenes/Game.tscn");
    }
}

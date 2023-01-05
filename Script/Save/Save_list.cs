using Godot;
using System;

public class Save_list : HBoxContainer
{
[Export] private int save_file_exist = 1;
    [Export] private string save_slot;
    private Signal signalcs;
    private Save_bluprint save_file;
    private SaveData savedata;
    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        save_file = GetNode<Save_bluprint>("/root/SaveBluprint");
        savedata = GetNode<SaveData>("/root/SaveData");

        File file = new File();

        for(int i=1; i<=3; i++)
        {
            string file_name = "Moon_0"+i;
            string path = "user://"+file_name+".json";
            if(file.FileExists(path)) SaveFileExist(file_name);
            else FileNotExist(file_name);
        }
    }
    void FileNotExist(string file)
    {
        PanelContainer panel = new PanelContainer();
        TextureButton create_save = new TextureButton();
        Label slot_name = new Label();
        CenterContainer conteiner = new CenterContainer();
        VBoxContainer separator = new VBoxContainer();

        panel.SizeFlagsHorizontal = 3;

        slot_name.Text = file;
        slot_name.Align = Label.AlignEnum.Center;

        //Set texure from texure button 
        create_save.TexturePressed = (Texture)GD.Load("res://Img/Buildings/factory_preset.png");
        create_save.TextureHover = (Texture)GD.Load("res://Img/Buildings/factory_hover.png");
        create_save.TextureNormal = (Texture)GD.Load("res://Img/Buildings/factory.png");

        conteiner.SizeFlagsVertical = 3;

        separator.AddChild(slot_name);
        conteiner.AddChild(create_save);
        separator.AddChild(conteiner);

        panel.AddChild(separator);
        AddChild(panel);


        create_save.Connect("pressed",this,nameof(CreateSaveFile),new Godot.Collections.Array{file});
    }

    void SaveFileExist(string file)
    {
        savedata.save_file = file;
        signalcs.EmitSignal(nameof(Signal.Load));

        PanelContainer panel = new PanelContainer();
        TextureButton open = new TextureButton();
        TextureButton delete_save = new TextureButton();
        Label slot_name = new Label();
        Label moon_name = new Label();
        CenterContainer conteiner1 = new CenterContainer();
        CenterContainer conteiner2 = new CenterContainer();
        VBoxContainer separator = new VBoxContainer();

        conteiner1.SizeFlagsVertical = 3;
        conteiner2.SizeFlagsVertical = 3;
        panel.SizeFlagsHorizontal = 3;

        slot_name.Text = "slot : " + savedata.save_file;
        slot_name.Align = Label.AlignEnum.Center;

        moon_name.Text = "Moon : " + savedata.moon_name + "size " + savedata.moon_size.ToString();
        moon_name.Align = Label.AlignEnum.Center;

        open.TexturePressed = (Texture)GD.Load("res://Img/Buildings/factory_preset.png");
        open.TextureHover = (Texture)GD.Load("res://Img/Buildings/factory_hover.png");
        open.TextureNormal = (Texture)GD.Load("res://Img/Buildings/factory.png");

        delete_save.TexturePressed = (Texture)GD.Load("res://Img/Buildings/factory_preset.png");
        delete_save.TextureHover = (Texture)GD.Load("res://Img/Buildings/factory_hover.png");
        delete_save.TextureNormal = (Texture)GD.Load("res://Img/Buildings/factory.png");

        separator.AddChild(slot_name);
        separator.AddChild(moon_name);
            
        conteiner1.AddChild(open);
        conteiner2.AddChild(delete_save);

        separator.AddChild(conteiner1);
        separator.AddChild(conteiner2);

        panel.AddChild(separator);
        AddChild(panel);

        delete_save.Connect("pressed",this,nameof(DeleteSave), new Godot.Collections.Array{file});
        open.Connect("pressed",this,nameof(Open));
    }

    void CreateSaveFile(string file)
    {
        savedata.save_file = file;
        GetTree().ChangeScene("res://Scenes/Save/Create_save.tscn");
    }

    void DeleteSave(string file)
    {
        string path = "user://"+file+".json";
        Directory save = new Directory();
        save.Remove(path);
        GetTree().ReloadCurrentScene();
    }

    void Open()
    {
        GetTree().ChangeScene("res://Scenes/Game.tscn");
    }
}

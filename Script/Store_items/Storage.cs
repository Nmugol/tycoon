using Godot;
using System;
using System.Collections.Generic;


public class Storage : PanelContainer
{
    private Signal signalcs;
    [Export] private Texture[] Item_icons;

    Dictionary<string,int> Items = new Dictionary<string, int>(){
            {"stone",100},
            {"irone",200}
        };

    [Export] private NodePath Storenode;
    
    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        signalcs.Connect("HaveResorces",this,"_HaveResorces");
        signalcs.Connect("Add_Resorses",this,"_Add_Resorses");

        Add_Store(Items);
    }

    private void Add_Store(Dictionary<string,int> Items)
    {
        Node Storeroom = GetNode<Node>(Storenode);
        GridContainer grid = new GridContainer();

        int i=0;
        foreach(var item in Items)
        {
            grid.AddChild(Add_store_items(item.Value,Item_icons[i]));
            i++;
        }

        Storeroom.AddChild(grid);
        grid.Columns=3;
    }

    private void Remowe_Store()
    {
        Node Storeroom = GetNode<Node>(Storenode);
        Node Store = Storeroom.GetChild(0);
        Storeroom.RemoveChild(Store);
        Store.QueueFree();
    }

    private Node Add_store_items(int how_many, Texture texture)
    {
        Label text = new Label();
        TextureRect icon = new TextureRect();
        HBoxContainer HBox = new HBoxContainer();

        text.Text = how_many.ToString();
        icon.Texture = texture;

        HBox.AddChild(icon);
        HBox.AddChild(text);

        return HBox;
    }

    private void _HaveResorces(string key, int value)
    {
        if(Items[key]>=value)
        {
            Items[key]-=value;
            signalcs.EmitSignal(nameof(Signal.YesHaveResorces));
            Remowe_Store();
            Add_Store(Items);
        }
        else
        signalcs.EmitSignal(nameof(Signal.NoHaveResorces));
    }

    private void _Add_Resorses(string key, int value)
    {
        Items[key]+=value;
        Remowe_Store();
        Add_Store(Items);
    }
}

using Godot;
using System;
using System.Collections.Generic;


public class Storage : PanelContainer
{
    [Export] private Texture[] Item_icons;
    
    public override void _Ready()
    {
        Dictionary<string,int> Items = new Dictionary<string, int>(){
            {"stone",100},
            {"irone",200}
        };

        Add_Store(Items, this);
    }

    private void Add_Store(Dictionary<string,int> Items, Node Storeroom)
    {
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

    private void Remowe_Store(Node Store, Node Storeroom)
    {
        Storeroom.RemoveChild(Store);
        Store.QueueFree();
    }

    public Node Add_store_items(int how_many, Texture texture)
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
}

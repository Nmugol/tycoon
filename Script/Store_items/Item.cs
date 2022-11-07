using Godot;
using System;

public class Item : HBoxContainer
{
    [Export]private NodePath Item_Img_path;
    [Export]private NodePath Item_stock_path;
    [Export]private Texture img;
    [Export]private int stock;
    public override void _Ready()
    {
        TextureRect Item = GetNode<TextureRect>(Item_Img_path);
        Label Stock = GetNode<Label>(Item_stock_path);

        Item.Texture=img;
        Stock.Text=stock.ToString();
    }
}

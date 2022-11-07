using Godot;
using System;

public class Storage : PanelContainer
{
    [Export] private NodePath Grid;
    [Export] private Texture[] Item_Img;
    [Export] private int[] Item_Counter;
    [Export] private NodePath[] Item_Img_Path;//path to texureract
    [Export] private NodePath[] Item_Counter_Path;//path to label
 
    public override void _Ready()
    {
        GridContainer _Grid = GetNode<GridContainer>(Grid);
        for(int i=0; i<Item_Counter_Path.Length; i++)
        {
            TextureRect _Item = GetNode<TextureRect>(Item_Img_Path[i]);
            Label _Counter = GetNode<Label>(Item_Counter_Path[i]);
            
            _Item.Texture=Item_Img[i];
            _Counter.Text=Item_Counter[i].ToString();

        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

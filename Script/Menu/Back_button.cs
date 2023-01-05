using Godot;
using System;

public class Back_button : Button
{
    [Export] int Sceen_to_back = 0;
    void _on_Back_button_pressed()
    {
        switch(Sceen_to_back)
        {
            case 0: GetTree().ChangeScene("res://Scenes/Menu/Main_menu.tscn"); break;
            case 1: GetTree().ChangeScene("res://Scenes/Save/Save_and_load.tscn"); break;
        }
    }
}

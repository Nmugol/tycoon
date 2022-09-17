using Godot;
using System;

public class Start_generate : Button
{   
    private Signal signalcs;

    [Export]private NodePath X_label_path;

    [Export]private NodePath Y_label_path;

    private int x_size=6;
    private int y_size=6;

    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
    }

    private void _on_Generate_button_down()
    {
        signalcs.EmitSignal(nameof(Signal.StartGenarate),x_size,y_size);
    }

    private void _on_X_sizie_slider_value_changed(int value)
    {
        Label X_labe = GetNode<Label>(X_label_path);
        X_labe.Text = value.ToString();
        x_size=value;
    }

    private void _on_Y_sizie_slider_value_changed(int value)
    {
        Label Y_labe = GetNode<Label>(Y_label_path);
        Y_labe.Text = value.ToString();
        y_size=value;
    }
}

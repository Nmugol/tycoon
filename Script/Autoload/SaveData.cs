using Godot;
using System;
using System.Collections.Generic;

public class SaveData : Node
{
    public Dictionary<string,int> Items = new Dictionary<string, int>
    {
        {"stone",100},
        {"irone",200}
    };

    public Dictionary<string,int[]> factory = new Dictionary<string, int[]>
    {
        //format name_of_factory, {how_many_factry, factory tyme}
        {"stonfactory",new int[] {0,0}}//resorses: stone
    };

    public string save_file ;
    public string moon_name ;
    public int moon_size;
    public int [,] buildings;
    public int [,] ground;
    public int [,] mountains;

}

using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Save_bluprint : Node
{
    private Signal signalcs;
    
    private SaveData savedata;

    public class Read_date
    {
        public string file { get; set; }
        public string moon { get; set; }
        public int size { get; set; }
        public int[][] buildings { get; set; }
        public int[][] ground { get; set; }
        public int[][] mountains { get; set; }
        public Dictionary<string, int> ilem_list { get; set; }
        public Dictionary<string, int[]> factory_list { get; set; }
    }

    public override void _Ready()
    {
        signalcs = GetNode<Signal>("/root/Signal");
        savedata = GetNode<SaveData>("/root/SaveData");

        signalcs.Connect("SaveData",this,"_SaveData");
        signalcs.Connect("Load",this,"_LoadData");
    }

    void _SaveData()
    {
        File save = new File();

        var error = save.Open("user://"+savedata.save_file+".json", File.ModeFlags.Write);

        if(error != 0) GD.Print("can't open file: "+ error);

        Dictionary<string,object> data = new Dictionary<string, object>
        {
            {"file",savedata.save_file},
            {"moon",savedata.moon_name},
            {"size",savedata.moon_size},
            {"buildings",savedata.buildings},
            {"ground",savedata.ground},
            {"mountains",savedata.mountains},
            {"ilem_list",savedata.Items},
            {"factory_list",savedata.factory}
        };

        var json_string = JsonConvert.SerializeObject(data);

        save.StoreString(json_string);
        save.Close();
    }

    void _LoadData()
    {
        Read_date data = new Read_date();
        File save = new File();

        save.Open("user://"+savedata.save_file+".json", File.ModeFlags.Read);

        var content = save.GetAsText();
        save.Close();

        
        data = JsonConvert.DeserializeObject<Read_date>(content);

        savedata.save_file = data.file;
        savedata.moon_name = data.moon;
        savedata.moon_size = data.size;

        int ln = data.buildings.Length;

        int[,] temp1 = new int[ln,ln];
        int[,] temp2 = new int[ln,ln];
        int[,] temp3 = new int[ln,ln];

        for(int i=0; i<ln; i++)
        {
            for(int j=0; j<ln; j++)
            {
                temp1[i,j] = data.buildings[i][j];
                temp2[i,j] = data.ground[i][j];
                temp3[i,j] = data.mountains[i][j];
            }
        }

        savedata.buildings = temp1;
        savedata.ground = temp2;
        savedata.mountains = temp3;
        
        savedata.Items = data.ilem_list;
        savedata.factory = data.factory_list;
    }
}

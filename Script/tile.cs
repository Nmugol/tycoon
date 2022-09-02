using Godot;
using System;

public class tile : Node2D
{
    private AnimatedSprite tile_sprite;
    private Random ran = new Random();
    public override void _Ready()
    {
        tile_sprite = GetNode<AnimatedSprite>("Sprite");

        int tile_variant = ran.Next(4) + 1;

        _TileSwitcher(tile_sprite,tile_variant);
    }

    private void _TileSwitcher( AnimatedSprite AS, int snum)
    {
        switch(snum)
        {
            case 1: 
                AS.Animation="grass";
            break;
            case 2: 
                AS.Animation="lava";
            break;
            case 3: 
                AS.Animation="sand";
            break;
            case 4: 
                AS.Animation="wather";
            break;
        }
    }
}

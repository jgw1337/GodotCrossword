using Godot;
using System;

public partial class Title : Node2D
{
    private void _on_play_pressed()
    {
        GetTree().ChangeSceneToFile("board.tscn");
    }
    
    private void _on_quit_pressed()
    {
        GetTree().Quit();
    }

}

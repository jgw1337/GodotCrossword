using Godot;
using System;

public partial class Board : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	private void _on_text_submitted(string new_text)
	{
		var puzzleScene = GD.Load<PackedScene>("res://puzzle.tscn");
		var puzzleInstance = puzzleScene.Instantiate();
		AddChild(puzzleInstance);
		
		// var puzzleScene = (PackedScene)ResourceLoader.Load("res://puzzle.tscn");
		// var puzzle = (LineEdit)puzzleScene.Instantiate();
		// AddChild(puzzle);
	}
	
	
}

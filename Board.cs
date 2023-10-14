using Godot;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class Board : Control
{
	private class Word
	{
		public string Answer { get; set; }
		public string Display { get; set; }
		public string Hint { get; set; }
	}

	private readonly List<Word> _words = new()
	{
		new Word { Answer = "Apple", Hint = "Golden delicious" },
		new Word { Answer = "Banana", Hint = "Don't eat with berries" },
		new Word { Answer = "Cherry", Hint = "Washington: I cannot tell a lie" },
	};

	private string _puzzleInstancePath = "Background/MarginContainer/Rows/GameInfo/PuzzleRows";
	private string _hiddenCharacter = "-";

	// private static readonly PackedScene PuzzleScene = GD.Load<PackedScene>("res://puzzle.tscn");
	// private Node _puzzleInstance = PuzzleScene.Instantiate();
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// foreach (var word in _words)
		// {
		// 	((Puzzle)_puzzleInstance).SetText(word.Hint, word.Answer);
		// 	GetNode("Background/MarginContainer/Rows/GameInfo/PuzzleRows").AddChild(_puzzleInstance);
		// }
		
		foreach (var word in _words)
		{
			var puzzleInstance = LoadPuzzleInstance();
			word.Display = Regex.Replace(word.Answer, "[A-Za-z ]", _hiddenCharacter);
			((Puzzle)puzzleInstance).SetText(word.Hint, word.Display);
			GetNode(_puzzleInstancePath).AddChild(puzzleInstance);
		}
	}

	private void _on_text_submitted(string new_text)
	{
		RemoveAllPuzzleRowsChildren();
		
		foreach (var word in _words)
		{
			var puzzleInstance = LoadPuzzleInstance();

			var temp = string.Empty;
			
			for (var index = 0; index < word.Answer.Length; index++)
			{
				temp += string.Equals(word.Answer[index].ToString(), new_text, StringComparison.InvariantCultureIgnoreCase) ? new_text : word.Display[index].ToString();
			}
			
			word.Display = temp;
			
			// word.Answer = word.Answer.Replace(new_text, "-", StringComparison.InvariantCultureIgnoreCase);
			
			((Puzzle)puzzleInstance).SetText(word.Hint, word.Display);
			GetNode(_puzzleInstancePath).AddChild(puzzleInstance);
		}

		// var puzzleScene = (PackedScene)ResourceLoader.Load("res://puzzle.tscn");
		// var puzzle = (LineEdit)puzzleScene.Instantiate();
		// AddChild(puzzle);
	}

	private void RemoveAllPuzzleRowsChildren()
	{
		var node = GetNode(_puzzleInstancePath);
		var children = node.GetChildren(); 
		
		foreach (var child in children)
		{
			node.RemoveChild(child);
		}
	}

	private static Node LoadPuzzleInstance()
	{
		var puzzleScene = GD.Load<PackedScene>("res://puzzle.tscn");
		return puzzleScene.Instantiate();

	}
}

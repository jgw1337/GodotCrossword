using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
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
		new Word { Answer = "Originator", Hint = "All classified material should be conspicuously marked by the:" },
		new Word { Answer = "Secret", Hint = "Information or material whose unauthorized disclosure could be expected to cause serious damage to national security." },
		new Word { Answer = "Evacuate", Hint = "If there is imminent danger to employees, the first step in an emergency is to:" },
		new Word { Answer = "Insider Threat", Hint = "Employees who have authorized access to information the company values most, and used that information to, wittingly or unwittingly, inflict harm to the organization or National Security." },
		new Word { Answer = "Spyware", Hint = "A type of malware installed on a computer without the knowledge of the owner in order to collect private information." },
		new Word { Answer = "Virtru", Hint = "Use this Gmail plug-in to send all CUI at VSolvit." },
		new Word { Answer = "PII", Hint = "Data that could potentially identify a specific individual." },
		new Word { Answer = "OPSEC", Hint = "A process by which we protect unclassified information that can be used against us." },
		new Word { Answer = "Security", Hint = "Cleared contractor employees are required to report adverse information to this department in VSolvit." },
		new Word { Answer = "Counterintelligence", Hint = "Information gathered and activities conducted to protect against espionage." },
		new Word { Answer = "Need to know", Hint = "Required prior to access to classified:" }
	};

	private string _puzzleInstancePath = "Background/MarginContainer/Rows/GameInfo/ScrollContainer/PuzzleRows";
	private string _hiddenCharacter = "-";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var word in _words)
		{
			var puzzleInstance = LoadPuzzleInstance();
			word.Display = Regex.Replace(word.Answer, "[A-Za-z0-9]", _hiddenCharacter);
			((Puzzle)puzzleInstance).SetText($"Hint: {word.Hint}", $"Answer: {word.Display}");
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

			((Puzzle)puzzleInstance).SetText($"Hint: {word.Hint}", $"Answer: {word.Display}");
			GetNode(_puzzleInstancePath).AddChild(puzzleInstance);
		}

		CheckForWinCondition();
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

	private void CheckForWinCondition()
	{
		var allDisplayedAnswers = string.Join("", _words.Select(x => x.Display).ToArray());

		if (!allDisplayedAnswers.Contains(_hiddenCharacter))
		{
			GetTree().ChangeSceneToFile("congrats.tscn");
		}
	}

	private static Node LoadPuzzleInstance()
	{
		var puzzleScene = GD.Load<PackedScene>("res://puzzle.tscn");
		return puzzleScene.Instantiate();
	}
}

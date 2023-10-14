using Godot;
using System;

public partial class Puzzle : VBoxContainer
{
	public void SetText(string hint, string answer)
	{
		((Label)GetNode("Hint")).Text = hint;
		((Label)GetNode("Word")).Text = answer;
	}
}

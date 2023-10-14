using Godot;
using System;

public partial class Input : LineEdit
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GrabFocus();
	}

	private void _on_text_submitted(string new_text)
	{
		Clear();
	}
}

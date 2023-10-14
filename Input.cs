using Godot;
using System;

public partial class Input : LineEdit
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GrabFocus();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_text_changed(string new_text)
	{
		new_text.ToUpper();
	}

	private void _on_text_submitted(string new_text)
	{
		Clear();
	}
}

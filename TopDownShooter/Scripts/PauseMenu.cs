using Godot;
using System;

public partial class PauseMenu : Control
{
	private Button resumeButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// for the convenience of the player when navigating the menu
		// via keyboard
		resumeButton = GetNode<Button>("VerticalLayoutParent/ResumeButton");
		resumeButton.GrabFocus();
	}
	
	private void _on_resume_button_pressed()
	{
		// Replace with function body.
		GD.Print("pressed resume");
		
		// TODO: unpause game and remove pause menu overlay
	}

	private void _on_options_button_pressed()
	{
		// segue to options menu
		GetTree().ChangeSceneToFile("res://Scenes/OptionsMenu.tscn");
	}

	private void _on_main_menu_button_pressed()
	{
		// segue to main menu
		GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
	}	
}

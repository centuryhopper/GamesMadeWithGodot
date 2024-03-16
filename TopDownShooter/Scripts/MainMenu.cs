using Godot;
using System;

public partial class MainMenu : Control
{
	private Button startButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// for the convenience of the player when navigating the menu
		// via keyboard
		startButton = GetNode<Button>("VerticalLayoutParent/StartButton");
		startButton.GrabFocus();
	}
	
	private void _on_start_button_pressed()
	{
		GD.Print("pressed start");
		GetTree().ChangeSceneToFile("res://Scenes/Level1.tscn");
	}
	
	private void _on_options_button_pressed()
	{
		GD.Print("pressed options");
		// var options = Load("")
	}
	
	private void _on_quit_button_pressed()
	{
		GD.Print("pressed quit");
		GetTree().Quit();
	}
}










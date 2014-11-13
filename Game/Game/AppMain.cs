using System;
using System.Collections.Generic;


using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

namespace Game
{
	public class AppMain
	{
		//Private Variables
		
		
		//Sce variables
		private static Sce.PlayStation.HighLevel.GameEngine2D.Scene 	gameScene;
		private static Sce.PlayStation.HighLevel.UI.Scene 				uiScene;	
		
		//Game Entities
		private static Player 			player;
		
		//Other Variables
		private static int 				screenWidth, screenHeight;
		private static GamePadButtons	gamePerd;
		
		public static void Main (string[] args)
		{
			Initialize ();
			
			//Game loop
			while (true) 
			{
				Update ();
				
				Director.Instance.Update();
				Director.Instance.Render();
				UISystem.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
			
			//Cleanup
			player.Dispose();
		}

		public static void Initialize ()
		{
			//Set up director + UISystem.
			Director.Initialize ();
			UISystem.Initialize(Director.Instance.GL.Context);
			
			//Set game scene
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			gameScene.Camera.SetViewFromViewport();
			screenWidth = Director.Instance.GL.Context.GetViewport().Width;
			screenHeight = Director.Instance.GL.Context.GetViewport().Height;	
			
			//Set the ui scene.
			uiScene = new Sce.PlayStation.HighLevel.UI.Scene();
			Panel panel  = new Panel();
			panel.Width  = Director.Instance.GL.Context.GetViewport().Width;
			panel.Height = Director.Instance.GL.Context.GetViewport().Height;
			
			uiScene.RootWidget.AddChildLast(panel);
			UISystem.SetScene(uiScene);
			
			//Run the scene.
			Director.Instance.RunWithScene(gameScene, true);
			
			//Create the player
			player = new Player(gameScene);
			
		}

		public static void Update ()
		{
			// Query gamepad for current state
			GamePadData data = GamePad.GetData (0);
			
			if(Input2.GamePad0.Left.Down)
				player.Move(-10.0f, 0.0f);
			
			if(Input2.GamePad0.Right.Down)
				player.Move(10.0f, 0.0f);
			
			if(Input2.GamePad0.Up.Down)
				player.Move(0.0f, 10.0f);
			
			if(Input2.GamePad0.Down.Down)
				player.Move(0.0f, -10.0f);
			
			
			
			if(Input2.GamePad0.Square.Down)
				player.Rotate();
			
			if(Input2.GamePad0.Circle.Down)
				player.Move(10.0f, 0.0f);
			
			if(Input2.GamePad0.Triangle.Down)
				player.Move(0.0f, 10.0f);
			
			if(Input2.GamePad0.Cross.Down)
				player.Move(0.0f, -10.0f);
						

			
		}
	}
}

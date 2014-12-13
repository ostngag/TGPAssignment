using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game
{
	public class AppMain
	{
		// Private Variables
		
		// Game Entities
		//private static GameScene currentScene;
		private static MenuScene menu;
		private static bool quitGame = false;
		
		
		public static void Main (string[] args)
		{
			Initialize ();
			
			// Game loop
			while (!quitGame) 
			{
				// Check for player input
				SystemEvents.CheckEvents ();
				
				Update ();
				
				Director.Instance.Update();
				Director.Instance.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
				
			 	//if(!menu.IsRunning)
				//	quitGame = true;
			}		
		}

		public static void Initialize ()
		{
			// Set up director + UISystem.
			Director.Initialize ();			
			// Run the scene.
			//currentScene = new GameScene();
			menu = new MenuScene();
			Director.Instance.RunWithScene(menu, true);		
		}

		public static void Update ()
		{
		}
	}
}

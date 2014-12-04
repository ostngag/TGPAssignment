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
		//Private Variables
		
		//Game Entities
		private static GameScene currentScene;
		
		public static void Main (string[] args)
		{
			Initialize ();
			
			//Game loop
			while (true) 
			{
				SystemEvents.CheckEvents ();
				Update ();
				
				Director.Instance.Update();
				Director.Instance.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
		}

		public static void Initialize ()
		{
			//Set up director + UISystem.
			Director.Initialize ();			
			//Run the scene.
			currentScene = new GameScene();
			Director.Instance.RunWithScene(currentScene, true);			
		}

		public static void Update ()
		{

			// if(Input2.GamePad0.Cross.Down)
			// 	player.Rotate(FMath.PI);	
			// 
			// 
			// if(Input2.GamePad0.Start.Down)
			// 	player.Attack();
		}
	}
}

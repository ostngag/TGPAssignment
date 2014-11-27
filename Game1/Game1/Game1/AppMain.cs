using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game1
{
	public class AppMain
	{
		////Private Variables
		//
		////Game Entities
		//private static GameScene currentScene;		
		////Other Variables
		//private static int screenWidth, screenHeight;		
		
		//private static GameState gameState;
		private static Level1State level1;
		
		public static void Main (string[] args)
		{
			Initialize ();

			while (true) {
				SystemEvents.CheckEvents ();
				Update ();
				
				Director.Instance.Update();
				Director.Instance.Render();
				
				Director.Instance.GL.Context.SwapBuffers();
				Director.Instance.PostSwap();
			}
		}

		#region Initialize
		public static void Initialize ()
		{
			//Set up director + UISystem.
			Director.Initialize ();					
			//Starts a new game by creating a new gameState
			level1 = new Level1State();
			Director.Instance.RunWithScene(level1, true);		
		}
		#endregion
		
		#region Update
		public static void Update (/*GameTime gameTime*/)
		{
			//// Query gamepad for current state
			//var gamePadData = GamePad.GetData (0);
			//
			//// Allows the game to exit
			//if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			//this.Exit();
			//CurrentState.Update(gameTime, viewportRect);
			//base.Update(gameTime);

		}
		#endregion
		
		//#region Draw
		//public static void Render ()
		//{
		//	// Clear the screen
		//	graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
		//	graphics.Clear ();
		//
		//	// Present the screen
		//	graphics.SwapBuffers ();
		//}
		//#endregion
	}
}

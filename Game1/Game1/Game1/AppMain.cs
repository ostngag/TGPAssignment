using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace Game1
{
	public class AppMain
	{
		
		public static void Main (string[] args)
		{
			Initialize ();
			//Starts a new game by creating a new gameState
			GameState gameState = new GameState();
			while (true) {
				SystemEvents.CheckEvents ();
				Update ();
				Render ();
			}
		}

		#region Initialize
		public static void Initialize ()
		{
			// Set up the graphics system
			graphics = new GraphicsContext ();
		}
		#endregion
		
		#region Update
		public static void Update (/*GameTime gameTime*/)
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData (0);
			
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			this.Exit();
			CurrentState.Update(gameTime, viewportRect);
			base.Update(gameTime);

		}
		#endregion
		
		#region Draw
		public static void Render ()
		{
			// Clear the screen
			graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			graphics.Clear ();

			// Present the screen
			graphics.SwapBuffers ();
		}
		#endregion
	}
}

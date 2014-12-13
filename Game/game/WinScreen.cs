using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Environment;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game
{
	public class WinScreen : Scene
	{
		private SpriteUV background = new SpriteUV(new TextureInfo("/Application/textures/winLoseScreens/Win/winScreen.png"));
		private SpriteUV continu = new SpriteUV(new TextureInfo("/Application/textures/winLoseScreens/Win/continueY.png")); 
		private SpriteUV quit = new SpriteUV(new TextureInfo("/Application/textures/winLoseScreens/Win/saveQuitY.png")); 
		private static TouchStatus  currentTouchStatus;
		
		public WinScreen()
		{
			this.Camera.SetViewFromViewport();
			
			// Background
			background.Quad.S.X = Director.Instance.GL.Context.GetViewport().Width;
			background.Quad.S.Y  = Director.Instance.GL.Context.GetViewport().Height;
			background.Position = new Vector2(0.0f,0.0f);
			AddChild(background);
			
			continu.Quad.S.X = 166;
			continu.Quad.S.Y = 81;
			continu.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width/2 - 83,330.0f);
			AddChild(continu);
			
			
			quit.Quad.S.X = 166;
			quit.Quad.S.Y = 81;
			quit.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width/2 - 83,130.0f);
			AddChild(quit);
			
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);
		}
		
		public override void Update (float dt)
        {
			var gamePadData = GamePad.GetData(0);
			List<TouchData> touches = Touch.GetData(0);
			
			foreach (TouchData data in touches)
			{
				currentTouchStatus = data.Status;
				float xPos = ((data.X + 0.5f) * background.Quad.S.X);
				float yPos = ((data.Y + 0.5f) * background.Quad.S.Y);
				
				if(data.Status == TouchStatus.Down)
				{
					if( xPos > continu.Position.X &&
					   xPos < continu.Position.X + continu.Quad.S.X &&
					   yPos > continu.Position.Y &&
					   yPos < continu.Position.Y + continu.Quad.S.Y)
					{
						//code for taking you back to the main menu
					}
				}
				
				if(data.Status == TouchStatus.Down)
				{
					if( xPos > quit.Position.X &&
						xPos < quit.Position.X + quit.Quad.S.X &&
					    yPos > quit.Position.Y &&
					    yPos < quit.Position.Y + quit.Quad.S.Y)
					{
						//code for reloading GameScene
					}
				}
			}
        }
        
        public override void Draw ()
        {
            
        }
        
        ~WinScreen()
        {
            
        }
		
	}

}




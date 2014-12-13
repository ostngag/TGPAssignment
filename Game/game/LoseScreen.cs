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
	public class LoseScreen : Scene
	{		
		private GameScene game;
		private MenuScene menu;
		
		private SpriteUV background = new SpriteUV(new TextureInfo("/Application/textures/winLoseScreens/Lose/loseScreen.png"));
		private SpriteUV restart = new SpriteUV(new TextureInfo("/Application/textures/winLoseScreens/Lose/restartY.png")); 
		private SpriteUV quit = new SpriteUV(new TextureInfo("/Application/textures/winLoseScreens/Lose/saveQuitY.png")); 
		private static TouchStatus  currentTouchStatus;
		
		public LoseScreen(GameScene game, MenuScene menu)
		{
			this.Camera.SetViewFromViewport();
			
			this.game = game;
			
			// Background
			background.Quad.S.X = Director.Instance.GL.Context.GetViewport().Width;
			background.Quad.S.Y  = Director.Instance.GL.Context.GetViewport().Height;
			background.Position = new Vector2(0.0f,0.0f);
			AddChild(background);
			
			restart.Quad.S.X = 166;
			restart.Quad.S.Y = 81;
			restart.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width/2 - 83,330.0f);
			AddChild(restart);
			
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
					if( xPos > restart.Position.X &&
					   xPos < restart.Position.X + restart.Quad.S.X &&
					   yPos > restart.Position.Y &&
					   yPos < restart.Position.Y + restart.Quad.S.Y)
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
						game.Reset();
						Director.Instance.ReplaceScene(menu);
						OnExit();
					}
				}
			}
        }
        
        public override void Draw ()
        {

        }
        
        ~LoseScreen()
        {
            
        }	
	}
}




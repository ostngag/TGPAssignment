using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Audio;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game
{
	public class MenuScene : Scene
	{	
		private SpriteUV title = new SpriteUV(new TextureInfo("/Application/textures/menuTextures/Title.png"));
		private SpriteUV background = new SpriteUV(new TextureInfo("/Application/textures/menuTextures/mainMenu.png"));
		private SpriteUV play = new SpriteUV(new TextureInfo("/Application/textures/menuTextures/playY.png")); 
		private SpriteUV options = new SpriteUV(new TextureInfo("/Application/textures/menuTextures/optionsY.png")); 
		private SpriteUV quit = new SpriteUV(new TextureInfo("/Application/textures/menuTextures/quitY.png")); 		
		private static TouchStatus  currentTouchStatus;
		private static GameScene startGame;
		private BgmPlayer musicPlayer = new Bgm("/Application/audio/menu.mp3").CreatePlayer();
		
		public bool quitGame = false;
		
		public MenuScene()
		{
			this.Camera.SetViewFromViewport();
			
			// Background
			background.Quad.S.X = Director.Instance.GL.Context.GetViewport().Width;
			background.Quad.S.Y  = Director.Instance.GL.Context.GetViewport().Height;
			background.Position = new Vector2(0.0f,0.0f);
			AddChild(background);
			
			// Title
			title.Quad.S.X = 500;
			title.Quad.S.Y = 81;
			title.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width/2 - 250.0f,430.0f);
			AddChild(title);
			
			//Play
			play.Quad.S.X = 166;
			play.Quad.S.Y = 81;
			play.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width/2 - 83,330.0f);
			AddChild(play);
			
			//Options - currently does nothing
			options.Quad.S.X = 166;
			options.Quad.S.Y = 81;
			options.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width/2 - 83,230.0f);
			AddChild(options);
			
			//Quit
			quit.Quad.S.X = 166;
			quit.Quad.S.Y = 81;
			quit.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width/2 - 83,130.0f);
			AddChild(quit);
			
			// Music
			musicPlayer.LoopStart = 0.545d;
			musicPlayer.LoopEnd= 125.40d;
			musicPlayer.Loop = true;
			musicPlayer.Play();
			
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);
		}
		
		public override void Update (float dt)
        {
			//highScore = startGame.GetHighScore();
			//currentScore = startGame.GetScore();
			
			var gamePadData = GamePad.GetData(0);
			List<TouchData> touches = Touch.GetData(0);
			
			foreach (TouchData data in touches)
			{
				currentTouchStatus = data.Status;
				float xPos = ((data.X + 0.5f) * background.Quad.S.X);
				float yPos = ((data.Y + 0.5f) * background.Quad.S.Y);
				
				if(data.Status == TouchStatus.Down)
				{
					if( xPos > play.Position.X &&
					   xPos < play.Position.X + play.Quad.S.X &&
					   yPos > play.Position.Y &&
					   yPos < play.Position.Y + play.Quad.S.Y)
					{
						quitGame = true;
					}
				}
				
				if(data.Status == TouchStatus.Down)
				{
					if( xPos > options.Position.X &&
						xPos < options.Position.X + options.Quad.S.X &&
					    yPos > options.Position.Y &&
					    yPos < options.Position.Y + options.Quad.S.Y)
					{
						//Currently does nothing
						Console.WriteLine("options");
					}
				}
					
				if(data.Status == TouchStatus.Down)
				{
					if( xPos > quit.Position.X &&
						xPos < quit.Position.X + quit.Quad.S.X &&
					    yPos > quit.Position.Y &&
					    yPos < quit.Position.Y + quit.Quad.S.Y)     
					{
						musicPlayer.Stop();
						musicPlayer.Dispose();
						startGame = new GameScene(this);
						play = new SpriteUV(new TextureInfo("/Application/textures/menuTextures/playO.png")); 
						Director.Instance.ReplaceScene(startGame);
					}
				}
			}
        }
		
		public override void OnEnter ()
        {
//            _songPlayer.Loop = true;
//            _songPlayer.Play();
        }
        
        public override void Draw ()
        {

        }
        
        ~MenuScene()
        {
            
        }
		
		public bool HasQuit(){ return quitGame; }		
	}
}




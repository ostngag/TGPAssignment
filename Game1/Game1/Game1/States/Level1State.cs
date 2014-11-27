using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game1
{
	public class Level1State : Scene
	{
		//private static Texture backgroundTexture;
		//private static Sprite player;
		//private static Sprite enemy;
		
		//Private Variables
		private Player 		player;
		private Enemy 		enemy;
		
		private TextureInfo textureInfo;
		private SpriteUV 	background;
		 
		private float 		previousAnalogRightX = 0.0f;
		private float       previousAnalogRightY = 0.0f;
		private bool 		attacking = false;
		
		public Level1State()
		{
			//this.gameState = gameState;
			
			this.Camera.SetViewFromViewport();			
			
			//X:960 Y:544
			textureInfo 		= new TextureInfo("/Application/textures/Greyscreen.png");
			background 			= new SpriteUV();
			background 			= new SpriteUV(textureInfo);
			background.Quad.S 	= textureInfo.TextureSizef;
			background.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width*0.5f,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			background.Pivot 	= new Vector2(background.Quad.S.X/2, background.Quad.S.Y/2);
			background.Scale 	= new Vector2(2.0f, 2.0f);
			AddChild(background);
			
			player = new Player(this);
			
			enemy = new Enemy(this, player);
			
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);
		}
		
		public static void LoadContent()
		{

		}
		
		public override void Update(float dt/*GameTime gameTime, Rectangle viewportRect*/)
		{
			//mozzie.Update(gameTime, viewportRect);
			//donut.Update(gameTime, viewportRect);
			//if (!mozzie.BoundingBox.Intersects(viewportRect))
			//theGame.CurrentState =new EndState(theGame);
			
			player.Update(dt);			
			enemy.Update(dt);
			
			// Query gamepad for current state
			var gamePadData = GamePad.GetData(0);
					
			player.Move(gamePadData.AnalogLeftX * 5.0f, -gamePadData.AnalogLeftY * 5.0f);
			
			Console.WriteLine("X: " + gamePadData.AnalogLeftX + "Y: " + gamePadData.AnalogLeftY);
			
			player.Rotate(gamePadData.AnalogRightX, gamePadData.AnalogRightY);
			
			//Console.WriteLine("X: " + previousAnalogRightX + " Y: " + previousAnalogRightY);
			
			if(gamePadData.AnalogRightX != 0 || gamePadData.AnalogRightY != 0)
				attacking = true;
			else
				attacking = false;
			
			if(attacking)
				player.Attack();
	
			// To detect if the player is moving the right analog
			previousAnalogRightX = gamePadData.AnalogRightX;
			previousAnalogRightY = gamePadData.AnalogRightY;				
		}
		
		public static void Draw()
		{
			
		}
		
		//actions used to change the state
		public void play()
		{
			LoadContent();
		}
		
		public void quit()
		{
			
		}
		
		public void win()
		{
			
		}
		
		public void lose()
		{
			
		}
	}
}


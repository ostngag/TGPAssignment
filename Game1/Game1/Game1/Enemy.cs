using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Input;

namespace Game1
{
	public class Enemy
	{
		//Private Variables
		
		
		//Sprite
		private static TextureInfo	textureInfo;
		private static SpriteUV 	sprite;
		private static float 			angle;
		
		//Character
		private static int healthPoints;
		private static float movementSpeed;
		
		//Player to chase
		Player player;
		
		public Enemy (Level1State currentState, Player player)
		{
			textureInfo  = new TextureInfo("/Application/textures/Bullet.png");
			
			//Sprite
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			sprite.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width*0.5f,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			sprite.Pivot 	= new Vector2(sprite.Quad.S.X/2, sprite.Quad.S.Y/2);
			sprite.Angle	= 0.0f;		
					
			//Load character values
			healthPoints = 100;
			movementSpeed = 5;
			
			this.player = player;
			
			currentState.AddChild(sprite);	
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float dt)
		{		
			float playerX = player.GetSprite().Position.X;
			float playerY = player.GetSprite().Position.Y;			
			float directionX = playerX - sprite.Position.X;
			float directionY = playerY - sprite.Position.Y;
			Move(directionX/100, directionY/100);						
		}
		
		public void Move(float x, float y)
		{
			sprite.Position = new Vector2(sprite.Position.X + x, sprite.Position.Y + y);
		}
		
		public void Rotate(float x, float y)
		{				
			if(x > 0.0f)
				if(y > 0.0f)
					sprite.Angle = (-3.0f * FMath.PI)/4.0f;
				else if(y < 0.0f)
						sprite.Angle = -FMath.PI/4.0f
					;
					 else
						sprite.Angle = -FMath.PI/2.0f;
			else if(x < 0.0f)
					if(y > 0.0f)
						sprite.Angle = (3.0f * FMath.PI)/4.0f;
					else if(y < 0.0f)
							sprite.Angle = FMath.PI/4.0f;
						 else
							sprite.Angle = FMath.PI/2.0f;
				else if(y > 0.0f)
						sprite.Angle = FMath.PI;
					 else if(y < 0.0f)
							sprite.Angle = 0;
		}				
	}
}


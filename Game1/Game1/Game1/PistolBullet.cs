using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game1
{

	
	public class PistolBullet
	{
		private bool fired = false;
		private SpriteUV sprite;
		private float bulletVelocityX, bulletVelocityY;
		public static int bulletInterval = 21;
		private static int speed = 10;
		
		public PistolBullet (PistolBulletSprite textureInfo, Level1State currentState)
		{	
			TextureInfo t = textureInfo.GetTextureInfo();
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(t);	
			sprite.Quad.S 	= t.TextureSizef;
			sprite.Position = new Vector2(-10000, 10000);
			
			currentState.AddChild(sprite);
		}
		
		public void Update()
		{
			if(fired)
				if(sprite.Position.X < 900 && sprite.Position.X >= 0 && sprite.Position.Y < 600 && sprite.Position.Y >= 0)
					sprite.Position = new Vector2(sprite.Position.X + bulletVelocityX, sprite.Position.Y + bulletVelocityY);
				else
				{
					sprite.Position = new Vector2(-10000, 10000);
					fired = false;
				}				
		}
		
		public void Fired(float posX, float posY, float velocityX, float velocityY)
		{ 
			sprite.Position = new Vector2(posX, posY);
			bulletVelocityX = velocityX;
			bulletVelocityY = velocityY;
			fired = true; 
		}
		
		public static int GetSpeed() { return speed; }
	}
}


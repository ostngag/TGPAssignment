using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Input;

namespace Game
{
	public class Enemy : Entity
	{
		//Private Variables
		
		
		//Sprite
		private static TextureInfo	textureInfo;
		public SpriteUV 	sprite;
		private float 			angle;
		
		//Character
		private int healthPoints;
		private static float movementSpeed;
		
		private bool alive = true;
		
		private bool collidedWithScene;

		
		//Player to chase
		Player player;
		
		public Enemy (GameScene currentScene, Player player, float posX, float posY)
		{
			textureInfo  = new TextureInfo("/Application/textures/Bullet.png");
			
			//Sprite
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			sprite.Position = new Vector2(posX, posY);
			sprite.Pivot 	= new Vector2(sprite.Quad.S.X/2, sprite.Quad.S.Y/2);
			sprite.Angle	= 0.0f;		
					
			//Load character values
			healthPoints = 100;
			movementSpeed = 5;
			
			this.player = player;
			
			currentScene.AddChild(sprite);	
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float dt)
		{		
			if(alive)
			{										
				float playerX = player.GetSprite().Position.X;
				float playerY = player.GetSprite().Position.Y;			
				float directionX = playerX - sprite.Position.X;
				float directionY = playerY - sprite.Position.Y;
				
				// If the enemy has collided with the scenery, then path around said object.
				if(collidedWithScene)
					PathFind();
				
				Move(directionX/100, directionY/100);
				Rotate(directionX, directionY);								
			}						
		}
		
		public void Move(float x, float y)
		{
			sprite.Position = new Vector2(sprite.Position.X + x, sprite.Position.Y + y);
		}
		
		public void Rotate(float x, float y)
		{		
			if(x < 0)
				sprite.Angle = FMath.Atan(y/x) + (FMath.PI/2);
			else
				sprite.Angle = FMath.Atan(y/x) - (FMath.PI/2);
		}		
		
		public void Killed()
		{
			alive = false;
			sprite.Position = new Vector2(2000, 2000);
		}
		
		public void PathFind()
		{
			
		}
		
		public bool Collision(SpriteUV sprite1, SpriteUV sprite2) //Collision detection
		{	
			//If any parts of the first sprite are OUTSIDE the second sprite, then false is returned
			if((sprite1.Position.X + sprite1.Quad.S.X) < sprite2.Position.X)
				return false;		
			
			if(sprite1.Position.Y > (sprite2.Position.Y + sprite2.Quad.S.Y))
				return false;
			
			if(sprite1.Position.X > (sprite2.Quad.S.X + sprite2.Position.X))
				return false;
			
			if((sprite1.Position.Y + sprite1.Quad.S.Y) < sprite2.Position.Y)
				return false;
			
			
			return true;
		}
	}
}

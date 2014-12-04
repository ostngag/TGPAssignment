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
		public SpriteUV 	sprite;
		private float 			angle;
		private static EntityType 	type = EntityType.enemy;
		//Character
		private int healthPoints;
		private  float movementSpeed;
		
		private bool alive = true;
		
		private bool collidedWithScene;

		
		//Player to chase
		Player player;
		
		public Enemy (GameScene currentScene, Player player, float posX, float posY, TextureInfo textureInfo)
		{			
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
		
		public override void Update(float dt)
		{		
			Console.WriteLine(dt);
			
			if(alive)
			{										
				float playerX = player.GetSprite().Position.X;
				float playerY = player.GetSprite().Position.Y;			
				float directionX = playerX - sprite.Position.X;
				float directionY = playerY - sprite.Position.Y;
				
				// If the enemy has collided with the scenery, then path around said object.
				//if(collidedWithScene)
					//PathFind();
				
				Move(directionX/100, directionY/100);
				Rotate(directionX, directionY);								
			}						
		}
		
		public override void Move(float x, float y)
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
		
		public void PathFind(SpriteUV sprite1, SpriteUV sprite2)
		{
			float xDiff = sprite2.Position.X - sprite1.Position.X;
			float yDiff = sprite2.Position.Y - sprite1.Position.Y;
				
			if(xDiff == 0)
			{
				sprite1.Position = new Vector2(sprite1.Position.X + 1.0f, sprite1.Position.Y + 1.0f);
				sprite2.Position = new Vector2(sprite2.Position.X - 1.0f, sprite2.Position.Y - 1.0f);
			}
			else	
			{
				float angle = FMath.Atan(yDiff/xDiff);
		
				float pushX = FMath.Cos(angle);
				float pushY = FMath.Sin(angle);
				
				Move(-pushX, -pushY);
			}
				//pistolBullet[bulletCount].Fired(gunPosX, gunPosY, bulletVelocityX, bulletVelocityY, sprite.Angle);								
		}
		
		public int Collision(SpriteUV sprite1, SpriteUV sprite2) //Collision detection
		{	
			//If any parts of the first sprite are OUTSIDE the second sprite, then false is returned
			if((sprite1.Position.X + sprite1.Quad.S.X) > sprite2.Position.X)
				return 1;		
			
			if(sprite1.Position.Y < (sprite2.Position.Y + sprite2.Quad.S.Y))
				return 2;
			
			if(sprite1.Position.X < (sprite2.Quad.S.X + sprite2.Position.X))
				return 3;
			
			if((sprite1.Position.Y + sprite1.Quad.S.Y) > sprite2.Position.Y)
				return 4;
			
			
			return 0;
		}
		
		public override void SortCollision(EntityType type)
		{
			if(type == EntityType.bullet)			
				Killed ();
						
			if(type == EntityType.scene)
			{
				// Path find
			}
		}
		
		public override SpriteUV GetSprite(){ return sprite; }
		
		public override EntityType GetEntityType(){ return type; }

	}
}


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
		private static EntityType 	type = EntityType.enemy;
		
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
					
			this.player = player;
			
			currentScene.AddChild(sprite);	
		}
		
		public override void Update(float dt)
		{		
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
		
		public override void Rotate(float x, float y)
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
		
		public override void SortCollision(Entity entity)
		{
			EntityType type = entity.GetEntityType();
			
			if(type == EntityType.bullet)			
				Killed ();
						
			if(type == EntityType.scene)
			{
				PathFind(sprite, entity.GetSprite());
			}
		}		
		
		public void PathFind(SpriteUV enemy, SpriteUV scenery)
		{		
			if((enemy.Position.X + enemy.Quad.S.X) > scenery.Position.X)
				Move(-5.0f, 0.0f);			
			else if(enemy.Position.Y < (scenery.Position.Y + scenery.Quad.S.Y))
					Move(0.0f, 5.0f);				
				else if(enemy.Position.X < (scenery.Quad.S.X + scenery.Position.X))
						Move(5.0f, 0.0f);					
					else if((enemy.Position.Y + enemy.Quad.S.Y) > scenery.Position.Y)
							Move(0.0f, -5.0f);	
		}
		
		public override SpriteUV GetSprite(){ return sprite; }
		
		public override EntityType GetEntityType(){ return type; }

	}
}


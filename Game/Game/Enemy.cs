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
		
		private bool alive = false;
		
		private static SoundManager sounds;	
		
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
			sprite.Scale    = new Vector2(2.0f, 2.0f);
					
			this.player = player;
			sounds = new SoundManager();
			
			currentScene.AddChild(sprite);	
		}
		
		public override void Update(float dt)
		{		
			if(alive)
			{										
				TrackPlayer();		
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
		
		public void TrackPlayer()
		{
			float xDiff = (sprite.Position.X + (sprite.Quad.S.X/2)) - (player.GetSprite().Position.X + (player.GetSprite().Quad.S.X/2));			
			float yDiff = (sprite.Position.Y + (sprite.Quad.S.Y/2)) - (player.GetSprite().Position.Y + (player.GetSprite().Quad.S.Y/2));
		
			if(!(xDiff == 0 || yDiff == 0))
			{
				if(yDiff > 0)
				{					
					float angle = FMath.PI - FMath.Atan(xDiff/yDiff);				
				 	Move(-3.0f * FMath.Sin(angle), -3.0f * -FMath.Cos(angle));			
				}
				else
				{
					float angle = FMath.Atan(xDiff/-yDiff);				
				 	Move(-3.0f * FMath.Sin(angle), -3.0f * -FMath.Cos(angle));	
				}	
				Rotate(-xDiff, -yDiff);			
			}		
		}
		
		public void Killed()
		{
			alive = false;
			sounds.Play(0, 1.0f, false);
			sprite.Position = new Vector2(10000, 10000);
		}	
		
		public override void SortCollision(Entity entity)
		{
			// This is so that the enemy instance doesn't collide with itself, as collision compares the list of entities with itself
			if(entity != this)
			{
				EntityType type = entity.GetEntityType();
				
				if(type == EntityType.bullet)			
					Killed ();
							
				if(type == EntityType.enemy || type == EntityType.scene)				
					PathFind(sprite, entity.GetSprite());							
			}
		}		
		
		public void PathFind(SpriteUV enemy, SpriteUV sprite2)
		{		
			float xDiff = (enemy.Position.X + (enemy.Quad.S.X/2)) - (sprite2.Position.X + (sprite2.Quad.S.X/2));			
			float yDiff = (enemy.Position.Y + (enemy.Quad.S.Y/2)) - (sprite2.Position.Y + (sprite2.Quad.S.Y/2));
		
			if(xDiff == 0 || yDiff == 0)
			{
				xDiff = 1.0f;
				yDiff = 1.0f;
			}
		
			if(yDiff > 0)
			{					
				float angle = FMath.PI - FMath.Atan(xDiff/yDiff);				
			 	Move(10.0f * FMath.Sin(angle), 10.0f * -FMath.Cos(angle));			
			}
			else
			{
				float angle = FMath.Atan(xDiff/-yDiff);				
			 	Move(10.0f * FMath.Sin(angle), 10.0f * -FMath.Cos(angle));	
			}	
		}
		
		public override SpriteUV GetSprite(){ return sprite; }
		
		public override EntityType GetEntityType(){ return type; }
		
		public bool IsAlive(){ return alive; }
		
		public void SetLife(bool setLife){ alive = setLife; }

	}
}


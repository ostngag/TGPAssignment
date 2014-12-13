using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Audio;

namespace Game
{
	public class Enemy : Entity
	{
		//Private Variables
		
		
		//Sprite
		public SpriteUV 	sprite;
		private static EntityType 	type = EntityType.enemy;
		
		private SpriteUV[,] spriteSheet;
		// Needed for animation when moving
		private int spriteTick = 0;
		private int spriteIndex1 = 0;
		private int spriteIndex2 = 0;
		private bool attacking = false;
		
		private bool alive = false;
		
		private static Sound file = new Sound("/Application/audio/aaahhh.wav");
		private static SoundPlayer sound = file.CreatePlayer();	
		
		private static float moveSpeed = 2.0f;
		private static int currentWave = 0;
		private static int numberOfLiveEnemies = 0;
		
		//Player to chase
		Player player;
		
		public Enemy (GameScene currentScene, Player player, float posX, float posY, SpriteUV[,] importedSpriteSheet)
		{			
			spriteSheet = importedSpriteSheet;
			
			//Sprite
			sprite	 			= new SpriteUV();
			sprite.TextureInfo	= spriteSheet[0,0].TextureInfo;	
			sprite.Quad.S 		= spriteSheet[0,0].TextureInfo.TextureSizef;
			sprite.Position 	= new Vector2(posX, posY);
			sprite.Pivot 		= new Vector2(sprite.Quad.S.X/2, sprite.Quad.S.Y/2);
			sprite.Angle		= 0.0f;
			sprite.Scale    	= new Vector2(2.0f, 2.0f);
					
			this.player = player;
			currentScene.AddChild(sprite);	
		}
		
		public override void Update(float dt, int wave)
		{		
			if(wave != currentWave)
			{
				currentWave = wave;
				SetProperties();
			}				
			
			if(alive)
			{			
				Animate();
				TrackPlayer();		
			}						
		}
		
		public void SetProperties()
		{
			if(currentWave % 5 == 0)
				moveSpeed += 1;
		}
		
		public override void Animate()
		{
			if(attacking)
			{
				if(spriteTick > 7)	
					spriteIndex1 = 0;
				else
					spriteIndex1 = 1;
			}
					
			if(spriteTick > 7)
			{
				spriteIndex2 = 1;
				
				if(spriteTick > 14)
					spriteTick = 0;
				
			}
			else
				spriteIndex2 = 0;
			
			sprite.TextureInfo = spriteSheet[spriteIndex1, spriteIndex2].TextureInfo;
		}
		
		public override void Move(float x, float y)
		{
			sprite.Position = new Vector2(sprite.Position.X + x, sprite.Position.Y + y);
			spriteTick++;
		}
		
		public override void Rotate(float x, float y)
		{		
			if(x < 0)
				sprite.Angle = FMath.Atan(y/x) + (FMath.PI/2);
			else
				sprite.Angle = FMath.Atan(y/x) - (FMath.PI/2);
		}
		
		public override void PlaySound(SoundPlayer sound, float volume)
		{
			base.PlaySound(sound, volume);
		}
		
		public void TrackPlayer()
		{
			float xDiff = (sprite.Position.X + (sprite.Quad.S.X/2)) - (player.GetSprite().Position.X + (player.GetSprite().Quad.S.X/2));			
			float yDiff = (sprite.Position.Y + (sprite.Quad.S.Y/2)) - (player.GetSprite().Position.Y + (player.GetSprite().Quad.S.Y/2));
			
			// If player is within proximity of the enemy, enable animation
			if((xDiff < 100.0f && xDiff > -100.0f) && (yDiff < 100.0f && yDiff > -100.0f))
				attacking = true;
			else
				attacking = false;
			
			if(!(xDiff == 0 || yDiff == 0))
			{
				if(yDiff > 0)
				{					
					float angle = FMath.PI - FMath.Atan(xDiff/yDiff);				
				 	Move(-moveSpeed * FMath.Sin(angle), -moveSpeed * -FMath.Cos(angle));			
				}
				else
				{
					float angle = FMath.Atan(xDiff/-yDiff);				
				 	Move(-moveSpeed * FMath.Sin(angle), -moveSpeed * -FMath.Cos(angle));	
				}	
				Rotate(-xDiff, -yDiff);			
			}	
		}
		
		public void Killed()
		{
			alive = false;
			numberOfLiveEnemies--;
			PlaySound(sound ,0.25f);
			player.AddToScore(100);
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
		
		public void Reset()
		{
			 alive = false;		
			 sprite.Position 	= new Vector2(-10000, 10000);
			 spriteTick = 0;
			 spriteIndex1 = 0;
			 spriteIndex2 = 0;
			 attacking = false;									
			 moveSpeed = 2.0f;
			 currentWave = 0;
			 numberOfLiveEnemies = 0;			
		}
		
		public override SpriteUV GetSprite(){ return sprite; }
		
		public override EntityType GetEntityType(){ return type; }
		
		public bool IsAlive(){ return alive; }
		
		public void SetLife(bool setLife){ alive = setLife; }
		
		public bool AreAllEnemiesDead(){ return numberOfLiveEnemies <= 0; }
		
		public void SetNumberOfLiveEnemies(int enemies){ numberOfLiveEnemies = enemies;}
	}
}


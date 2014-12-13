using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Audio;

namespace Game
{
	public class Player : Entity
	{
		// Private
		// Sprite
		private static SpriteUV 	charSprite;
		// Since the main sprite rotates, a collision box is needed for accurate calculations
		private static SpriteUV		collisionBox;
		private static EntityType 	type = EntityType.player;
		
		private SpriteUV[,] spriteSheet;
		// Needed for animation when moving
		private int spriteTick = 0;
		private int spriteIndex1 = 0;
		private int spriteIndex2 = 0;
		private bool attacking = false;
		private bool moving = false;
		
		private static bool alive = true;
		
		// Public
		public Weapon weapon;

		public Player (GameScene currentScene, SpriteUV[,] importedSpriteSheet, TextureInfo textureInfo2)
		{	
			
			spriteSheet = importedSpriteSheet;
			
			// Main Sprite
			charSprite	 			= new SpriteUV();
			charSprite.TextureInfo  = spriteSheet[0,0].TextureInfo;	
			charSprite.Quad.S 		= spriteSheet[0,0].TextureInfo.TextureSizef;
			charSprite.Position 	= new Vector2(Director.Instance.GL.Context.GetViewport().Width*0.5f,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			charSprite.Pivot 		= new Vector2(charSprite.Quad.S.X/2, (charSprite.Quad.S.Y/2) - 20.0f);
			charSprite.Angle		= -FMath.PI/2.0f;					
			
			collisionBox	 		= new SpriteUV();
			collisionBox 			= new SpriteUV(textureInfo2);	
			collisionBox.Quad.S 	= new Vector2(50.0f, 50.0f);
			collisionBox.Position 	= new Vector2((Director.Instance.GL.Context.GetViewport().Width*0.5f) - charSprite.Pivot.X,
			                                     (Director.Instance.GL.Context.GetViewport().Height*0.5f) - charSprite.Pivot.Y);		
			
			weapon					 = new Weapon(currentScene);
	
			//sounds					 = new SoundManager();
			
			currentScene.AddChild(charSprite);
			currentScene.AddChild(collisionBox);
		}
		
		public override void Update(float dt, int wave)
		{			
			weapon.Update(dt, wave);
			Animate();
		}
		
		public override void Move(float x, float y)
		{
			charSprite.Position = new Vector2(charSprite.Position.X + x, charSprite.Position.Y + y);
			collisionBox.Position = charSprite.Position;
		}
		
		public override void Animate()
		{
			if(spriteTick > 7)
			{
				spriteIndex2 = 1;
				
				if(spriteTick > 14)
					spriteTick = 0;
				
			}
			else
				spriteIndex2 = 0;
			
			if(moving)
				spriteTick++;
			
			if(attacking)
				spriteIndex1 = 1;
			else
				spriteIndex1 = 0;
			
			charSprite.TextureInfo = spriteSheet[spriteIndex1, spriteIndex2].TextureInfo;				
		}
		
		public override void Rotate(float x, float y)
		{				
		//	if(x > 0.0f)
		//		if(y > 0.0f)
		//			charSprite.Angle = (-3.0f * FMath.PI)/4.0f;
		//		else if(y < 0.0f)
		//				charSprite.Angle = -FMath.PI/4.0f;
		//			 else
		//				charSprite.Angle = -FMath.PI/2.0f;
		//	else if(x < 0.0f)
		//			if(y > 0.0f)
		//				charSprite.Angle = (3.0f * FMath.PI)/4.0f;
		//			else if(y < 0.0f)
		//					charSprite.Angle = FMath.PI/4.0f;
		//				 else
		//					charSprite.Angle = FMath.PI/2.0f;
		//		else if(y > 0.0f)
		//				charSprite.Angle = FMath.PI;
		//			 else if(y < 0.0f)
		//					charSprite.Angle = 0;
			
			// Allows for 2D omnidirectional weapon aiming
			if(x < 0)
				charSprite.Angle = FMath.Atan(-y/x) + (FMath.PI/2);
			else if(x > 0)
					charSprite.Angle = FMath.Atan(-y/x) - (FMath.PI/2);
				else if(x == 0)
						if(y < 0)
							charSprite.Angle = 0;
						else if(y > 0)
								charSprite.Angle = FMath.PI;
							else if(y == 0)
								charSprite.Angle = charSprite.Angle;	
		}				
		
		public void Attack()
		{
			weapon.Fire(charSprite);
		}
		
		public void Killed()
		{
			alive = false;
		}
		
		public override void SortCollision(Entity entity)
		{
			EntityType type = entity.GetEntityType();
			
			if(type == EntityType.enemy)			
				Killed ();
			
			if(type == EntityType.scene)			
				PathFind(collisionBox, entity.GetSprite());		
		}			
		
		public void PathFind(SpriteUV playerSprite, SpriteUV scenery)
		{			
			// The player will be pushed back once collided with the scenery
			
			float xDiff = (playerSprite.Position.X + (playerSprite.Quad.S.X/2)) - (scenery.Position.X + (scenery.Quad.S.X/2));			
			float yDiff = (playerSprite.Position.Y + (playerSprite.Quad.S.Y/2)) - (scenery.Position.Y + (scenery.Quad.S.Y/2));
		
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
				
		public override SpriteUV GetSprite (){ return collisionBox; }
		
		public override EntityType GetEntityType(){ return type; }
		
		public bool IsAlive(){ return alive; }
		
		public void SetAttacking(bool attacking){ this.attacking = attacking; }
		
		public void SetMoving(bool moving){ this.moving = moving; }
	}
}


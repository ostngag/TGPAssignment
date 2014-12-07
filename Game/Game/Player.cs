using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Input;

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

		private static bool alive = true;
		
		// Public
		public Weapon weapon;

		public Player (GameScene currentScene, TextureInfo textureInfo1, TextureInfo textureInfo2)
		{			
			// Sprite
			charSprite	 			= new SpriteUV();
			charSprite 				= new SpriteUV(textureInfo1);	
			charSprite.Quad.S 		= textureInfo1.TextureSizef;
			charSprite.Position 	= new Vector2(Director.Instance.GL.Context.GetViewport().Width*0.5f,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			charSprite.Pivot 		= new Vector2(charSprite.Quad.S.X/2, charSprite.Quad.S.Y/2);
			charSprite.Angle		= -FMath.PI/2.0f;		
						
			collisionBox	 		= new SpriteUV();
			collisionBox 			= new SpriteUV(textureInfo2);	
			collisionBox.Quad.S 	= new Vector2(50.0f, 50.0f);
			collisionBox.Position 	= new Vector2((Director.Instance.GL.Context.GetViewport().Width*0.5f) - charSprite.Pivot.X,
			                                     (Director.Instance.GL.Context.GetViewport().Height*0.5f) - charSprite.Pivot.Y);		
			
			weapon					 = new Weapon(currentScene);
			
			currentScene.AddChild(charSprite);
			currentScene.AddChild(collisionBox);
		}
		
		public override void Update(float dt)
		{			
			weapon.Update(dt);
			Console.WriteLine("X: " + charSprite.Position.X + " Y: " + charSprite.Position.Y);
		}
		
		public override void Move(float x, float y)
		{
			charSprite.Position = new Vector2(charSprite.Position.X + x, charSprite.Position.Y + y);
			collisionBox.Position = charSprite.Position;
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
				
		public void ChangeWeapon(int weaponNo)
		{ 
			weapon.ChooseWeapon(weaponNo); 
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
	}
}


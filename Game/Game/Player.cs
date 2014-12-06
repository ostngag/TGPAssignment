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
		//Private Variables
		
		//Sprite
		private static SpriteUV 	charSprite;
		private static EntityType 	type = EntityType.player;
		
		//Character
		public Weapon weapon;
		
		public float lastMoveX = 0;
		public float lastMoveY = 0;
		
		private static bool collided = false;
		private static bool alive = true;
		
		public Player (GameScene currentScene, TextureInfo textureInfo)
		{			
			//Sprite
			charSprite	 		= new SpriteUV();
			charSprite 			= new SpriteUV(textureInfo);	
			charSprite.Quad.S 	= textureInfo.TextureSizef;
			charSprite.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width*0.5f,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			charSprite.Pivot 	= new Vector2(charSprite.Quad.S.X/2, charSprite.Quad.S.Y/2);
			charSprite.Angle	= -FMath.PI/2.0f;		
			
			weapon = new Weapon(currentScene);
			
			currentScene.AddChild(charSprite);			
		}
		
		public override void Update(float dt)
		{			
			//Health
			//Momentum
			weapon.Update(dt);
		}
		
		public override void Move(float x, float y)
		{
			charSprite.Position = new Vector2(charSprite.Position.X + x, charSprite.Position.Y + y);
			
			if(!collided)
			{
				lastMoveX = x;
				lastMoveY = y;
			}	
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
				PathFind(charSprite, entity.GetSprite());		
		}			
		
		public void PathFind(SpriteUV playerSprite, SpriteUV scenery)
		{		
			if((playerSprite.Position.X + playerSprite.Quad.S.X) > scenery.Position.X)
				Move(-5.0f, 0.0f);			
			else if(playerSprite.Position.Y < (scenery.Position.Y + scenery.Quad.S.Y))
					Move(0.0f, 5.0f);				
				else if(playerSprite.Position.X < (scenery.Quad.S.X + scenery.Position.X))
						Move(5.0f, 0.0f);					
					else if((playerSprite.Position.Y + playerSprite.Quad.S.Y) > scenery.Position.Y)
							Move(0.0f, -5.0f);	
		}
		
		public override SpriteUV GetSprite (){ return charSprite; }
		
		public override EntityType GetEntityType(){ return type; }
		
		public bool IsAlive(){ return alive; }
	}
}


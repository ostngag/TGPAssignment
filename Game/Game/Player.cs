using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Input;

namespace Game
{
	public class Player
	{
		//Private Variables
		
		
		//Sprite
		private static TextureInfo	textureInfo;
		private static SpriteUV 	charSprite;
		private static float 			angle;
		private static SpriteUV		bulletSprite;
		
		//Character
		private static int healthPoints;
		private static float movementSpeed;
		private static float velocity;
		private static bool meleeMode;
		public Weapon weapon;
		private static int secondWeaponAmmo;
		private static int thirdWeaponAmmo;
		private static int fourthWeaponAmmo;
		
		public Player (GameScene currentScene)
		{
			textureInfo  = new TextureInfo("/Application/textures/Char.png");
			
			//Sprite
			charSprite	 		= new SpriteUV();
			charSprite 			= new SpriteUV(textureInfo);	
			charSprite.Quad.S 	= textureInfo.TextureSizef;
			charSprite.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width*0.5f,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			charSprite.Pivot 	= new Vector2(charSprite.Quad.S.X/2, charSprite.Quad.S.Y/2);
			charSprite.Angle	= 0.0f;
		
			
			//Load character values
			healthPoints = 100;
			movementSpeed = 5;
			meleeMode = false;
			
			weapon = new Weapon(currentScene);
			
			currentScene.AddChild(charSprite);			
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update(float dt)
		{			
			//Health
			//Momentum
			Console.WriteLine(dt);
			weapon.Update();
		}
		
		public void Move(float x, float y)
		{
			charSprite.Position = new Vector2(charSprite.Position.X + x, charSprite.Position.Y + y);
		}
		
		public void Rotate(float x, float y)
		{				
			if(x > 0.0f)
				if(y > 0.0f)
					charSprite.Angle = (-3.0f * FMath.PI)/4.0f;
				else if(y < 0.0f)
						charSprite.Angle = -FMath.PI/4.0f;
					 else
						charSprite.Angle = -FMath.PI/2.0f;
			else if(x < 0.0f)
					if(y > 0.0f)
						charSprite.Angle = (3.0f * FMath.PI)/4.0f;
					else if(y < 0.0f)
							charSprite.Angle = FMath.PI/4.0f;
						 else
							charSprite.Angle = FMath.PI/2.0f;
				else if(y > 0.0f)
						charSprite.Angle = FMath.PI;
					 else if(y < 0.0f)
							charSprite.Angle = 0;
			
//			if(x == 0.0f)
//				if(y == -1.0f)
//					charSprite.Angle = 0.0f;
//				else
//					charSprite.Angle = FMath.PI;			
//		 	else if(x > 0.25f)			
//					if(x < 0.5f && x > 0.25f)
//						if(y == 0.0f)
//							charSprite.Angle = -FMath.PI/2.0f;
//						else if(y < 0.0f && y > -0.25f)
//								charSprite.Angle = (-3.0f * FMath.PI)/8.0f;
//							else if(y > 0f && y < 0.25f)
//									charSprite.Angle = (-5.0f * FMath.PI)/8.0f;
//								else if(y < -0.25f && y > -0.5f)
//										charSprite.Angle = -FMath.PI/4.0f;
//									else if(y > 0.25f && y < 0.5f)
//											charSprite.Angle = (3.0f * FMath.PI)/4.0f;
//										else if(y < -0.5f && y > -0.75f)
//												charSprite.Angle = -FMath.PI/8.0f;
//											else if(y > 0.5f && y < 0.75f)
//													charSprite.Angle = (-7.0f * FMath.PI)/8.0f;
//					else if(x < 0.75f && x > 0.5f)
//						if(y == 0.0f)
//							charSprite.Angle = -FMath.PI/2.0f;
//						else if(y < 0.0f&& y > -0.25f)
//								charSprite.Angle = (-3.0f * FMath.PI)/8.0f;
//							else if(y > 0.0f && y < 0.25f)
//									charSprite.Angle = (-5.0f * FMath.PI)/8.0f;
//								else if(y < -0.25f && y > -0.5f)
//										charSprite.Angle = -FMath.PI/4.0f;
//									else if(y > 0.25f && y < 0.5f)
//											charSprite.Angle = (-3.0f * FMath.PI)/4.0f;
//						else if(x > 0.75f && x < 1.0f)
//							if(y == 0.0f)
//								charSprite.Angle = -FMath.PI/2.0f;
//							else if(y < 0.0f && y > -0.25f)
//									charSprite.Angle = (-3 * FMath.PI)/8.0f;
//								else if(y > 0.0f && y < 0.25f)
//										charSprite.Angle = (-5.0f * FMath.PI)/8.0f;
//							else if(x == 1.0f)
//								charSprite.Angle = -FMath.PI/2.0f;		
//				else if(x < -0.25f)			
//						if(x > -0.5f && x < -0.25f)
//							if(y == 0.0f)
//								charSprite.Angle = FMath.PI/2.0f;
//							else if(y < 0.0f && y > -0.25f)
//									charSprite.Angle = (3.0f * FMath.PI)/8.0f;
//								else if(y > 0f && y < 0.25f)
//										charSprite.Angle = (5.0f * FMath.PI)/8.0f;
//									else if(y < -0.25f && y > -0.5f)
//											charSprite.Angle = FMath.PI/4.0f;
//										else if(y > 0.25f && y < 0.5f)
//												charSprite.Angle = (3.0f * FMath.PI)/4.0f;
//											else if(y < -0.5f && y > -0.75f)
//													charSprite.Angle = FMath.PI/8.0f;
//												else if(y > 0.5f && y < 0.75f)
//														charSprite.Angle = (7.0f * FMath.PI)/8.0f;
//						else if(x > -0.75f && x < -0.5f)
//							if(y == 0.0f)
//								charSprite.Angle = FMath.PI/2.0f;
//							else if(y < 0.0f&& y > -0.25f)
//									charSprite.Angle = (3.0f * FMath.PI)/8.0f;
//								else if(y > 0.0f && y < 0.25f)
//										charSprite.Angle = (5.0f * FMath.PI)/8.0f;
//									else if(y < -0.25f && y > -0.5f)
//											charSprite.Angle = FMath.PI/4.0f;
//										else if(y > 0.25f && y < 0.5f)
//												charSprite.Angle = (3.0f * FMath.PI)/4.0f;
//							else if(x < -0.75f && x > -1.0f)
//								if(y == 0.0f)
//									charSprite.Angle = FMath.PI/2.0f;
//								else if(y < 0.0f && y > -0.25f)
//										charSprite.Angle = (3 * FMath.PI)/8.0f;
//									else if(y > 0.0f && y < 0.25f)
//											charSprite.Angle = (5.0f * FMath.PI)/8.0f;
//										else if(x == 1.0f)
//											charSprite.Angle = FMath.PI/2.0f;
//
			
			//Console.WriteLine(charSprite.Angle);			
			
			//if(y == 1 || y == -1)
			//	charSprite.Angle = charSprite.Angle * -1;			


		}				
				
		public void ChangeWeapon(int weaponNo)
		{ 
			weapon.ChooseWeapon(weaponNo); 
		}
		
		public void Attack()
		{
			if(meleeMode)
			{
				//melee attack
			}
			else
			{
				//fire weapon
				weapon.Fire(charSprite);
			}
		}
		
<<<<<<< HEAD
		public void Killed()
		{
		}
		
=======
>>>>>>> origin/Rui
		public SpriteUV GetSprite(){ return charSprite; }
	}
}


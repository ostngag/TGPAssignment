using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game
{
	public class Player
	{
		//Private Variables
		
		//Sprite
		private static TextureInfo	textureInfo;
		private static SpriteUV 	charSprite;
		private static SpriteUV		bulletSprite;
		
		//Character
		private static int healthPoints;
		private static float movementSpeed;
		private static float velocity;
		private static bool meleeMode;
		private static Weapon weapon;
		private static int secondWeaponAmmo;
		private static int thirdWeaponAmmo;
		private static int fourthWeaponAmmo;

		public Player (Scene scene, int hp, int ms)
		{
			textureInfo  = new TextureInfo("/Application/textures/Plaiyah.png");
			
			//Sprite
			charSprite	 		= new SpriteUV();
			charSprite 			= new SpriteUV(textureInfo);	
			charSprite.Quad.S 	= textureInfo.TextureSizef;
			charSprite.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width*0.5f,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			charSprite.Pivot 	= new Vector2(charSprite.Quad.S.X/2, charSprite.Quad.S.Y/2);
			charSprite.Angle = 0.0f;
			
			//Load character values
			healthPoints = hp;
			movementSpeed = ms;
			meleeMode = false;
			
			weapon = new Weapon();
			
			//Add to the current scene.
			scene.AddChild(charSprite);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Update()
		{
			//Update sprite
			
			//Set weapon
			
			
			//Health
			//Momentum
		}
		
		public void Move(float x, float y)
		{
			charSprite.Position = new Vector2(charSprite.Position.X + x, charSprite.Position.Y + y);
		}
		
		public void Rotate(float angle)
		{
			charSprite.Angle = angle;
		}				
				
		public void ChangeWeapon(int weaponNo)
		{ 
			weapon.chooseWeapon(weaponNo); 
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
				weapon.fire(charSprite.Position.X, charSprite.Position.Y, charSprite.Angle, charSprite.Quad.S.Y);
			}
		}
	}
}


using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game
{
	public class Weapon
	{
		// Private variables
		
		
		// Sprites
		private static TextureInfo	textureInfo;
		public SpriteUV 	currentBullet;
		private  SpriteUV 	pistolBullet;
				
		// Other
		private static int weaponChosen;		
		private static int reloadSpeed;
		private static bool fired;
		
		private static float bulletVelocityX, bulletVelocityY;
			
		public Weapon(GameScene currentScene)
		{
			// Load all weapon bullets
			
			textureInfo  = new TextureInfo("/Application/textures/Bullet.png");	
			//Current bullet values
			currentBullet	 		= new SpriteUV();
			currentBullet 			= new SpriteUV(textureInfo);	
			currentBullet.Quad.S 	= textureInfo.TextureSizef;
			
			// Pistol bullet		
			pistolBullet	 		= new SpriteUV();
			pistolBullet 			= new SpriteUV(textureInfo);	
			pistolBullet.Quad.S 	= textureInfo.TextureSizef;
			
			weaponChosen = 1;
			currentBullet = pistolBullet;
			
			currentScene.AddChild(currentBullet);
		}
		
		public void Update()
		{
			if(fired)
			{
				if(currentBullet.Position.X < 900 && currentBullet.Position.X >= 0 && currentBullet.Position.Y < 600 && currentBullet.Position.Y >= 0)
					currentBullet.Position = new Vector2(currentBullet.Position.X + bulletVelocityX, currentBullet.Position.Y + bulletVelocityY);
				else
				{
					fired = false;
				}
			}
			
			//Console.WriteLine("X: " + currentBullet.Position.X + " Y: " + currentBullet.Position.Y);
		}
		
		public void ChooseWeapon(int weaponNo)
		{
			if(weaponNo == 2)
			{
				// SMG
			}
			else if(weaponNo == 3)
			{
				// Shotgun
			}
			else if(weaponNo == 4)
			{
				// Laser
			}
			else
			{
				// Pistol
				currentBullet = pistolBullet;
			}
		}
		
		public void Fire(float x, float y, float angle, float radius)
		{			
			if(!fired)
			{
				fired = true;
				float gunPosX = x + (radius * FMath.Cos(angle));
				float gunPosY = y + (radius * FMath.Sin(angle));
				currentBullet.Position = new Vector2(gunPosX, gunPosY);			
				bulletVelocityX = FMath.Cos(angle / 10.0f);
				bulletVelocityY = FMath.Sin(angle / 10.0f);
			}
		}
	}
}


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
		
		public  PistolBullet[] 	pistolBullet;
			
		public PistolBulletSprite pistolSprite;
		
		// Other
		private static int bulletCount = 0;
			
		public Weapon(GameScene currentScene)
		{
			pistolSprite = new PistolBulletSprite();
			
			
			// Load all weapon bullets
			
			
			// Pistol bullet
			pistolBullet 			= new PistolBullet[30];
			
			for(int i = 0; i < 30; i++)
			{
				pistolBullet[i] = new PistolBullet(pistolSprite, currentScene);
			}	
		}
		
		public void Update(float dt)
		{			
			for(int i = 0; i < 30; i++)			
				pistolBullet[i].Update(dt);				
			
			PistolBullet.bulletInterval++;
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
				//currentBullet = pistolBullet;
			}
		}
		
		public void Fire(SpriteUV sprite)
		{			
			if(PistolBullet.bulletInterval > PistolBullet.GetSpeed())
			{
				if(bulletCount >= 30)
				bulletCount = 0;
			
				float x = sprite.Position.X + (sprite.Quad.S.X/2);
				float y = sprite.Position.Y + (sprite.Quad.S.Y/2);
						
				float angle = sprite.Angle + (FMath.PI/2);
				float radius = sprite.Quad.S.Y/2;
	
				float gunPosX = x + (radius * FMath.Cos(angle));
				float gunPosY = y + (radius * FMath.Sin(angle));		
				float bulletVelocityX = FMath.Cos(angle) * 10;
				float bulletVelocityY = FMath.Sin(angle) * 10;
				
				pistolBullet[bulletCount].Fired(gunPosX, gunPosY, bulletVelocityX, bulletVelocityY, sprite.Angle);
				
				PistolBullet.bulletInterval = 0;
				
				bulletCount++;
			}				
		}
	}
}


using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Audio;

namespace Game
{	
	public class PistolBullet : Entity
	{
		private bool fired = false;
		public SpriteUV sprite;
		private float bulletVelocityX, bulletVelocityY;
		
		// A count which when is larger than firingSpeed, will allow a bullet to fire
		public static int bulletInterval = 6;
		// This is the time interval between each shot of the weapon
		private static int firingSpeed = 5;
		
		private static EntityType 	type = EntityType.bullet;
		
		private static Sound file = new Sound("/Application/audio/fire.wav");
		private static SoundPlayer sound = file.CreatePlayer();	
		
		public PistolBullet (PistolBulletSprite textureInfo, GameScene currentScene)
		{	
			TextureInfo t = textureInfo.GetTextureInfo();
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(t);	
			sprite.Quad.S 	= t.TextureSizef;
			sprite.Position = new Vector2(-10000, -10000);
			
			currentScene.AddChild(sprite);
		}
		
		public override void Update(float dt, int wave)
		{
			if(fired)
				sprite.Position = new Vector2(sprite.Position.X + bulletVelocityX, sprite.Position.Y + bulletVelocityY);			
		}
		
		public override void Move(float x, float y)
		{
			sprite.Position = new Vector2(sprite.Position.X + x, sprite.Position.Y + y);
		}
		
		public void Fired(float posX, float posY, float velocityX, float velocityY, float angle)
		{ 
			sprite.Position = new Vector2(posX, posY);
			sprite.Angle = angle;
			bulletVelocityX = velocityX;
			bulletVelocityY = velocityY;
			fired = true;
			PlaySound(sound, 0.1f);
		}
		
		public void HitEntity()
		{
			sprite.Position = new Vector2(-10000, -10000);
			fired = false;
		}
		
		public override void SortCollision(Entity entity)
		{
			EntityType type = entity.GetEntityType();
			
			if(type == EntityType.enemy || type == EntityType.scene)			
				HitEntity();
		}
		
		public static int GetSpeed() { return firingSpeed; }
		
		public override SpriteUV GetSprite(){ return sprite; }
		
		public override EntityType GetEntityType(){ return type; }
	}
}


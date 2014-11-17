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
		private static SpriteUV 	sprite;
		private static TextureInfo	textureInfo;
		
		//Character
		private static int healthPoints;
		private static float movementSpeed;
		private static bool meleeMode;
		private static int weaponChoice;
		private static int secondWeaponAmmo;
		private static int thirdWeaponAmmo;
		private static int fourthWeaponAmmo;

		public Player (Scene scene)
		{
			textureInfo  = new TextureInfo("/Application/textures/Plaiyah.png");
			
			sprite	 		= new SpriteUV();
			sprite 			= new SpriteUV(textureInfo);	
			sprite.Quad.S 	= textureInfo.TextureSizef;
			sprite.Position = new Vector2(Director.Instance.GL.Context.GetViewport().Width*0.5f,Director.Instance.GL.Context.GetViewport().Height*0.5f);
			sprite.Pivot 	= new Vector2(sprite.Quad.S.X/2, sprite.Quad.S.Y/2);
			sprite.Angle = 0.0f;
			
			//Add to the current scene.
			scene.AddChild(sprite);
		}
		
		public void Dispose()
		{
			textureInfo.Dispose();
		}
		
		public void Move(float x, float y)
		{
			sprite.Position = new Vector2(sprite.Position.X + x, sprite.Position.Y + y);
		}
		
		public void Rotate(float angle)
		{
			sprite.Angle = angle;
		}
	}
}


using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Input;

namespace Game1
{
	public class PistolBulletSprite
	{
		private static TextureInfo	textureInfo;
		
		public PistolBulletSprite ()
		{
			textureInfo  = new TextureInfo("/Application/textures/Bullet.png");	
		}
		
		public TextureInfo GetTextureInfo() { return textureInfo; }
	}
}


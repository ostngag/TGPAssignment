using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game
{
	public class SceneObstruction : Entity
	{
		private SpriteUV 	objectSprite;
		private Collision	collision;	
		
		private static EntityType	type = EntityType.scene;
		
		public SceneObstruction (GameScene currentScene, TextureInfo texture, float posX, float posY, float sizeX, float sizeY)
		{			
			//Sprite
			objectSprite	 		= new SpriteUV();
			objectSprite 			= new SpriteUV(texture);	
			objectSprite.Quad.S 	= new Vector2(sizeX, sizeY);
			objectSprite.Position	= new Vector2(posX, posY);
			
			currentScene.AddChild(objectSprite);
		}
		
		public override void Update(float dt)
		{
			
		}	
		
		public SpriteUV GetSprite(){ return objectSprite; }		
	}
}


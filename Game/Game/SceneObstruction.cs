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
		
		private static EntityType	type = EntityType.scene;
		
		private float posX, posY;	
		
		public SceneObstruction (GameScene currentScene, TextureInfo texture, float posX, float posY, float sizeX, float sizeY)
		{			
			//Sprite
			objectSprite	 		= new SpriteUV();
			objectSprite 			= new SpriteUV(texture);	
			objectSprite.Quad.S 	= new Vector2(sizeX, sizeY);
			objectSprite.Position	= new Vector2(posX, posY);
			
			this.posX = posX;
			this.posY = posY;
			
			currentScene.AddChild(objectSprite);
		}
		
		public override void Update(float dt, int wave)
		{
		}	
		
		public override void Move(float x, float y)
		{
			objectSprite.Position = new Vector2(objectSprite.Position.X + x, objectSprite.Position.Y + y);
		}
		
		public override void SortCollision(Entity entity)
		{
			EntityType type = entity.GetEntityType();	
		}
		
		public void Reset()
		{
			objectSprite.Position	= new Vector2(posX, posY);
		}
		
		public override SpriteUV GetSprite(){ return objectSprite; }
		
		public override EntityType GetEntityType(){ return type; }
	}
}

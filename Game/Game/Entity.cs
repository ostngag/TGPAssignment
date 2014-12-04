using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Input;
namespace Game
{
	public abstract class Entity
	{
		// All entities exhibit this properties
		protected SpriteUV 	sprite;
		protected int baseClassAccess;
		
		// This enum is needed to sort out collisions between entities
		public enum EntityType {player, enemy, bullet, scene};
		protected EntityType type;
		
		public Entity ()
		{			
		}		
		
		public virtual void Update(float dt)
		{
		}
		
		public virtual void Move(float x, float y)
		{
		}
		
		public virtual void Rotate(float x, float y)
		{				
		}
		
		public virtual bool Collision(SpriteUV sprite1, SpriteUV sprite2) // Collision detection
		{	
			// If any parts of the first sprite are OUTSIDE the second sprite, then false is returned
			if((sprite1.Position.X + sprite1.Quad.S.X) < sprite2.Position.X)
				return false;		
			
			if(sprite1.Position.Y > (sprite2.Position.Y + sprite2.Quad.S.Y))
				return false;
			
			if(sprite1.Position.X > (sprite2.Quad.S.X + sprite2.Position.X))
				return false;
			
			if((sprite1.Position.Y + sprite1.Quad.S.Y) < sprite2.Position.Y)
				return false;
			
			
			return true;
		}
		
		public virtual void SortCollision(EntityType type){}
		
		public virtual SpriteUV GetSprite(){ return sprite; }
		
		public virtual EntityType GetEntityType(){ return type; }
	}
}


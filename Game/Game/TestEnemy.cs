using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Input;

namespace Game
{
	public class TestEnemy: Entity
	{
		public TestEnemy ()
		{
		}
		
		public override bool Collision (SpriteUV sprite1, SpriteUV sprite2)
		{
			return base.Collision (sprite1, sprite2);
			// bool returnValue = base.Collision (sprite1, sprite2);
		}
	}
}


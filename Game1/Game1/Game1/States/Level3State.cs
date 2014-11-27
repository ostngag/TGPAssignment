using System;

namespace Game1
{
	public class Level3State : State
	{
		//private static Texture backgroundTexture;
		//private static Sprite player;
		//private static Sprite enemy;
		
		public Level3State(GameState gameState)
		{
			this.gameState = gameState;
		}
		
		public static void LoadContent()
		{
			
		}
		
		public override void Update(/*GameTime gameTime, Rectangle viewportRect*/)
		{
			//mozzie.Update(gameTime, viewportRect);
			//donut.Update(gameTime, viewportRect);
			//if (!mozzie.BoundingBox.Intersects(viewportRect))
			//theGame.CurrentState =new EndState(theGame);
		}
		
		public static void Draw()
		{
			
		}
		
		//actions used to change the state
		public void play()
		{
			
		}
		
		public void quit()
		{
			
		}
		
		public void win()
		{
			
		}
		
		public void lose()
		{
			
		}
	}
}


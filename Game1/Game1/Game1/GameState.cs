using System;

namespace Game1
{
	public class GameState
	{
		State MENU;
		State LEVEL1;
		State LEVEL2;
		State LEVEL3;
		State WIN;
		State LOSE;
		
		State state = MENU;
		
		public GameState()
		{
			MENU = new MenuState(this);
			LEVEL1 = new Level1State(this);
			LEVEL2 = new Level2State(this);
			LEVEL3 = new Level3State(this);
			WIN = new WinState(this);
			LOSE = new LoseState(this);
		}
		
		public void play()
		{
			state.play();
		
		}
		
		public void quit()
		{
			state.quit();
		}
		
		public void win()
		{
			state.win();
		}
		
		public void lose()
		{
			state.lose();
		}
	}
}
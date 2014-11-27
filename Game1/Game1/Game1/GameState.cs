using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game1
{
	public class GameState// : Scene
	{
		//State MENU;
		//State LEVEL1;
		//State LEVEL2;
		//State LEVEL3;
		//State WIN;
		//State LOSE;
		//
		//State state;
		//
		Level1State state;
		
		public GameState()
		{
			//MENU = new MenuState(this);
			//LEVEL1 = new Level1State(this);
			//LEVEL2 = new Level2State(this);
			//LEVEL3 = new Level3State(this);
			//WIN = new WinState(this);
			//LOSE = new LoseState(this);
			//state = new Level1State(this);
		}
		
		public void Update()
		{
			//state.Update(dt);
		}
		
		public void play()
		{
			//state.Play();
		
		}
		
		public void quit()
		{
			//state.Quit();
		}
		
		public void win()
		{
			//state.Win();
		}
		
		public void lose()
		{
			//state.Lose();
		}
	}
}
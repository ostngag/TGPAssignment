using System;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.Core.Input;

namespace Game
{
	public class EnemySpawner
	{
		private Enemy[] enemy;
		private Vector2 spawnPos;
		private const int NUMBER_OF_ENEMIES = 20;
		
		// FOR DEBUGGING LOCATION OF SPAWNERS
		//private SpriteUV block;
		
		// The amount of enemies spawned per second
		private float spawnSpeed = 1;
		private int spawnCounter = 0;
		private float spawnTimer = 0;
		
		// The Coordinates from which the spawner shall function, else no enemies are spawned
		private float conditionX;
		private float conditionY;
		private bool canSpawn = false;
		
		public EnemySpawner(GameScene currentScene, Player player, TextureInfo textureInfo, float posX, float posY, float conditionX, float conditionY)
		{
			// Enemies
			enemy 			= new Enemy[NUMBER_OF_ENEMIES];						
			spawnPos 		= new Vector2(posX, posY);
			
			for(int i = 0; i < NUMBER_OF_ENEMIES; i++)
			{
				// Spawn enemies far away and bring into arena when possible
				enemy[i] 	= new Enemy(currentScene, player, -10000, 10000, textureInfo);
			}	
			
			this.conditionX = conditionX;
			this.conditionY = conditionY;
			
			// FOR DEBUGGING LOCATION OF SPAWNERS (ADD ADDITIONAL TextureInfo PARAMETER NEEDED)
			//block 			= new SpriteUV(tex);	
			//block.Scale    = new Vector2(50.0f, 50.0f);
			//block.Position = new Vector2(posX + 25.0f, posY + 25.0f);		
			//currentScene.AddChild(block);
			
		}
		
		public void Update(float dt)
		{			
			// FOR DEBUGGING LOCATION OF SPAWNERS (ADD ADDITIONAL TextureInfo PARAMETER NEEDED)
			//block.Position = new Vector2(spawnPos.X + 25.0f, spawnPos.Y + 25.0f);
			//Console.WriteLine(spawnPos);
			
			CheckSpawnCondition();
			
			spawnTimer += dt;
			
			if(spawnTimer > (1/spawnSpeed) && canSpawn)
			{
				// Spawn dead enemies back into the arena
				if(!enemy[spawnCounter].IsAlive())
				{
					enemy[spawnCounter].GetSprite().Position = new Vector2(spawnPos.X, spawnPos.Y);
					enemy[spawnCounter].SetLife(true);
				}
									
				spawnCounter++;
				spawnTimer = 0;
				
				if(spawnCounter >= NUMBER_OF_ENEMIES)
					spawnCounter = 0;
			}
		}
		
		public void CheckSpawnCondition()
		{
			if(conditionX != 0)
			{
				if(conditionX > 0)
					if(spawnPos.X > conditionX)
						canSpawn = true;
					else
						canSpawn = false;
				else
					if(spawnPos.X < conditionX)
						canSpawn = true;
					else
						canSpawn = false;	
			}
			
			if(conditionY != 0)
			{
				if(conditionY > 0)
					if(spawnPos.Y > conditionY)
						canSpawn = true;
					else
						canSpawn = false;
				else
					if(spawnPos.Y < conditionY)
						canSpawn = true;
					else
						canSpawn = false;
			}
		}
		
		public Enemy GetEnemy(int i){ return enemy[i]; }
		
		public int GetNoOfEnemies(){ return NUMBER_OF_ENEMIES; }
		
		public void AddToPosition(float posX, float posY)
		{
			spawnPos = new Vector2(spawnPos.X + posX, spawnPos.Y + posY);
		}
	}
}


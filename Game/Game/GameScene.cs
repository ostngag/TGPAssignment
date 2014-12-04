using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;

namespace Game
{
	public class GameScene : Scene
	{
		//Private Variables
		private Player 		player;
		private Enemy[] 	enemy;
		
		private TextureInfo textureInfo;
		private SpriteUV 	background;
		
		private SceneObstruction[] sceneObject;
		
		private Collision collChecker;
		
		private bool 		attacking = false;
		
		//enum EntityType{bullet, enemy, player, scene};
		
		public GameScene()
		{
			this.Camera.SetViewFromViewport();			
			
			collChecker = new Collision();
			
			// Setup all entities and sprites
			
			// Background
			textureInfo 		= new TextureInfo("/Application/textures/Arena2.jpg");
			background 			= new SpriteUV();
			background 			= new SpriteUV(textureInfo);
			background.Quad.S 	= textureInfo.TextureSizef;
			background.Position = new Vector2(250.0f, 0.0f);
			background.Pivot 	= new Vector2(background.Quad.S.X/2, background.Quad.S.Y/2);
			background.Scale 	= new Vector2(1.0f, 1.0f);
			AddChild(background);
			
			// Player
			textureInfo 		= new TextureInfo("/Application/textures/Player.png");
			player 				= new Player(this, textureInfo);
			
			// Enemies
			textureInfo 		= new TextureInfo("/Application/textures/Bullet.png");
			enemy 				= new Enemy[20];
			Random dX = new Random();
			Random dY = new Random();
			float x = 0;
			float y = 0;
			
			for(int i = 0; i < 20; i++)
			{
				x = (float)(10000 * dX.NextDouble());
				y = (float)(10000 * dY.NextDouble());
				
				enemy[i] = new Enemy(this, player, x, y, textureInfo);
			}	
			
			// All scene objects
			textureInfo			= new TextureInfo("/Application/textures/Black.png");
			
			SetUpSceneObjects(textureInfo);
			
			
			
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);
		}
		
		public void Dispose()
		{
			// Cleanup
			textureInfo.Dispose();
		}
		
		public void SetUpSceneObjects(TextureInfo textureInfo)
		{
			sceneObject 		= new SceneObstruction[24];
			
			sceneObject[0] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + ((150 * background.Scale.X) /2), 
			                                      background.Position.Y + ((177 * background.Scale.Y) /2),
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[1] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + ((170 * background.Scale.X) /2),
			                                      background.Position.Y + ((177 * background.Scale.Y) /2),
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[2] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + ((170 * background.Scale.X) /2),
			                                      background.Position.Y + ((157 * background.Scale.Y) /2),
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			
		    sceneObject[3] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + ((295 * background.Scale.X) /2),
			                                      background.Position.Y + ((177 * background.Scale.Y) /2),
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[4] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + ((275 * background.Scale.X) /2),
												  background.Position.Y + ((177 * background.Scale.Y) /2),
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[5] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + ((275 * background.Scale.X) /2),
			                                      background.Position.Y + ((157 * background.Scale.Y) /2),
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			
		    sceneObject[6] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + ((295 * background.Scale.X) /2),
			                                      background.Position.Y + ((282 * background.Scale.Y) /2),
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[7] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + ((275 * background.Scale.X) /2),
			                                      background.Position.Y + ((282 * background.Scale.Y) /2),
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[8] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + ((275 * background.Scale.X) /2),
			                                      background.Position.Y + ((302 * background.Scale.Y) /2),
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			
			sceneObject[9] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + ((150 * background.Scale.X) /2),
			                                      background.Position.Y + ((282 * background.Scale.Y) /2),
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[10] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + ((170 * background.Scale.X) /2),
			                                      background.Position.Y + ((282 * background.Scale.Y) /2),
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			sceneObject[11] = new SceneObstruction(this, textureInfo,
			                                      background.Position.X + ((170 * background.Scale.X) /2),
			                                      background.Position.Y + ((302 * background.Scale.Y) /2),
			                                      background.Scale.X * 10, background.Scale.Y * 10);
			
			sceneObject[12] = new SceneObstruction(this, textureInfo,
                                      			  background.Position.X + ((47 * background.Scale.X) /2),
			                                      background.Position.Y + ((27 * background.Scale.Y) /2),
                                      			  background.Scale.X * 29, background.Scale.Y * 29);
			
			sceneObject[13] = new SceneObstruction(this, textureInfo,
				                      			  background.Position.X + ((362 * background.Scale.X) /2),
			                                      background.Position.Y + ((27 * background.Scale.Y) /2),
				                      			  background.Scale.X * 29, background.Scale.Y * 29);
			
			sceneObject[14] = new SceneObstruction(this, textureInfo,
				                      			  background.Position.X + ((362 * background.Scale.X) /2),
			                                      background.Position.Y + ((397 * background.Scale.Y) /2),
				                      			  background.Scale.X * 29, background.Scale.Y * 29);
			
			sceneObject[15] = new SceneObstruction(this, textureInfo,
				                      			  background.Position.X + ((47 * background.Scale.X) /2),
			                                      background.Position.Y + ((397 * background.Scale.Y) /2),
				                      			  background.Scale.X * 29, background.Scale.Y * 29);
			
		}
		
		public override void Update(float dt)
		{		
			// Add all entities to a list
			List<Entity> entities = new List<Entity>();	
			
			entities.Add(player);
			
			for(int i = 0; i < 20; i++)			
				entities.Add(enemy[i]);
			
			for(int i = 0; i < 16; i++)			
				entities.Add(sceneObject[i]);
			
			foreach(PistolBullet bullets in player.weapon.pistolBullet)
				entities.Add(bullets);
				
			
			// Update all entities
			foreach(Entity entries in entities)
				entries.Update(dt);
			
			Input (entities);
			
			CheckCollisions(entities);
		}
		
		public void Input(List<Entity> entities)
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData(0);
			
			if(player.IsAlive())
			{							
				// Movement
				foreach(Entity entries in entities)
					if(!entries.Equals(player))
						entries.Move(gamePadData.AnalogLeftX * -5.0f, gamePadData.AnalogLeftY * 5.0f);
				
				background.Position = new Vector2(background.Position.X - (gamePadData.AnalogLeftX * 5.0f),
				                                  background.Position.Y - (-gamePadData.AnalogLeftY * 5.0f));
							
				// Player Rotation
				player.Rotate(gamePadData.AnalogRightX, gamePadData.AnalogRightY);
				
				// Firing your weapon
				if(gamePadData.AnalogRightX != 0 || gamePadData.AnalogRightY != 0)
					attacking = true;
				else
					attacking = false;
				
				if(attacking)
					player.Attack();
			}
		}
		
		public void CheckCollisions(List<Entity> entities)
		{		
			foreach(Entity currentEntries in entities)
				foreach(Entity otherEntries in entities)
					if(collChecker.calcCollision(currentEntries.GetSprite(), otherEntries.GetSprite()))
					{
						currentEntries.SortCollision(otherEntries.GetEntityType());
						otherEntries.SortCollision(currentEntries.GetEntityType());
					}
		}
	}
}

	
